using Jtech.Common.DataStore.Interface;
using Jtech.Common.DataStore.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq.Dynamic.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data;
using Esprima.Ast;
using System.Reflection;
using static MassTransit.ValidationResultExtensions;

namespace Jtech.Common.DataStore
{
    public class Store<U> : Store
    {
        public Store(IMongoDatabase database) : base(database)
        {
        }

        public Store(DbContext myDbContext) : base(myDbContext)
        {
        }
    }

    public class Store
    {
        private readonly DbContext dbContext;
        private readonly IMongoDatabase _database;

        public Store(IMongoDatabase database)
        {
            _database = database;
        }

        public Store(DbContext myDbContext)
        {
            dbContext = myDbContext;
        }

        private bool IsIEntity<T>() where T : class
        {

            return typeof(T).GetInterface(typeof(IEntity).FullName) != null;
        }

        private string GetCollectionName<T>()
        {
            TableAttribute tableAttr = (TableAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));

            return (tableAttr == null) ? typeof(T).Name.ToLower() : tableAttr.Name;
        }

        private int GetSkip(int pageIndex, int take)
        {
            return (pageIndex - 1) * take;
        }

        public async Task<string> Create<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(T));

            if (dbContext == null)
                await this._database.GetCollection<T>(GetCollectionName<T>()).InsertOneAsync(entity);
            else
            {
                dbContext.Add<T>(entity);
                await dbContext.SaveChangesAsync();
            }

            if (this.IsIEntity<T>())
                return await Task.FromResult(((IEntity)entity).Id);
            else
                return await Task.FromResult("ID");
        }

        public async Task<long> Update<T>(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(T));

            if (dbContext == null)
            {
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(existingentity => ((IEntity)existingentity).Id, ((IEntity)entity).Id);
                var ret = await this._database.GetCollection<T>(GetCollectionName<T>()).ReplaceOneAsync(filter, entity);
                return await Task.FromResult(ret.ModifiedCount);
            }
            else
            {
                dbContext.Update(entity);
                var ret = await dbContext.SaveChangesAsync();
                return await Task.FromResult(ret);
            }
        }

        public async Task<long> DeleteById<T>(string id) where T : class
        {
            if (dbContext == null)
            {
                FilterDefinition<T> filter = Builders<T>.Filter.Eq(entity => ((IEntity)entity).Id, id);
                var ret = await this._database.GetCollection<T>(GetCollectionName<T>()).DeleteOneAsync(filter);
                return await Task.FromResult(ret.DeletedCount);
            }
            else
            {
                var entity = await this.GetById<T>(id);
                if (entity == null)
                    return 0;

                dbContext.Remove(entity);
                return await Task.FromResult(await dbContext.SaveChangesAsync());
            }
        }

        public virtual async Task<T?> GetById<T>(string id, params Expression<Func<T, object>>[] childIncludes) where T : class
        {
            if (!IsIEntity<T>())
                await Task.FromException(new Exception($"{typeof(T).Name} not implement IEntity."));
            
            return await Task.FromResult( GetByCondition<T>(entity => ((IEntity)entity).Id == id, childIncludes)
                .Result!.FirstOrDefault());
        }

        public async Task<IList<T>?> GetByCondition<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] childIncludes) where T : class
        {
            if (dbContext == null)
                return await Task.FromResult( this._database.GetCollection<T>(GetCollectionName<T>()).Find(filter).ToList());
            else
                return await Task.FromResult(DbContextQuery<T>(filter, null, childIncludes).ToList());
        }

        public async Task<IReadOnlyCollection<T>> GetAll<T>(string? orderBy = null, params Expression<Func<T, object>>[] childIncludes) where T : class
        {
            return await GetByCondition<T>((Expression<Func<T, bool>>)null, orderBy, childIncludes);
        }

        public async Task<IReadOnlyCollection<T>> GetByCondition<T>(string? filter, string? orderBy = null, params Expression<Func<T, object>>[] childIncludes) where T : class
        {
            Expression<Func<T, bool>>? filterExpr = (Expression<Func<T, bool>>)null;

            if (filter != null)
                filterExpr = DynamicExpressionParser.ParseLambda<T, bool>(ParsingConfig.Default, false, filter, new object[0]);


            return await GetByCondition<T>(filterExpr, orderBy, childIncludes);
        }

        public async Task<IReadOnlyCollection<T>> GetByCondition<T>(Expression<Func<T, bool>> filter, string? orderBy = null, params Expression<Func<T, object>>[] childIncludes) where T : class
        {
            if (dbContext != null)
                return await DbContextQuery(filter, orderBy, childIncludes)
                                .ToListAsync();
            else
                return await MongoQuery<T>(filter, orderBy)
                                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPagingByCondition<T>(Pagination page, string? filter, string? orderBy = null, params Expression<Func<T, object>>[] childIncludes) where T : class
        {
            if (page == null)
                throw new Exception($"{nameof(Pagination)} is null.");

            Expression<Func<T, bool>>? filterExpr = (Expression<Func<T, bool>>)null;

            if (filter != null)
                filterExpr = DynamicExpressionParser.ParseLambda<T, bool>(ParsingConfig.Default, false, filter, new object[0]);

            List<T> ret;
            if (dbContext == null)
            {
                var result = MongoQuery(filterExpr, orderBy);
                page.TotalItems = result.CountDocuments();
                ret = await GetMongoPage(result, page, page.Current).ToListAsync();
            }
            else
            {
                var result = DbContextQuery(filterExpr, orderBy, childIncludes);
                page.TotalItems = result.Count();
                ret = await GetDbContextPage<T>(result, page, page.Current).ToListAsync();
            }
            return await Task.FromResult(ret);
        }

        public async Task<object?> SQLExecuteSaclar<T>(string query)
        {
            if (this._database == null)
                await Task.FromException<NotSupportedException>(new NotSupportedException("Mongodb not support."));

            object? result = null;
            using (var command = this.dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                dbContext.Database.OpenConnection();
                result = command.ExecuteScalar();
                dbContext.Database.CloseConnection();
            }
            return await Task.FromResult(result);
        }

        public async Task<int> SQLExecuteCommand(string query, Action<DbParameterCollection>? StoreParametersAction = null)
        {
            if (this._database == null)
                await Task.FromException<NotSupportedException>(new NotSupportedException("Mongodb not support."));

            int result = 0;
            using (var command = this.dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                if (StoreParametersAction == null)
                    command.CommandType = CommandType.Text;
                else
                {
                    command.CommandType = CommandType.StoredProcedure;
                    StoreParametersAction.Invoke(command.Parameters);
                }
                dbContext.Database.OpenConnection();
                result = command.ExecuteNonQuery();
                dbContext.Database.CloseConnection();
            }
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<T>> SQLQueryRaw<T>(string query)
        {
            IEnumerable<T>? result = null;
            if (this._database == null)
                await Task.FromException<NotSupportedException>(new NotSupportedException("Mongodb not support."));

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                dbContext.Database.OpenConnection();

                using (IDataReader rd = command.ExecuteReader())
                    result = rd.AutoMap<T>();

                dbContext.Database.CloseConnection();
            }
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<T>> SQLQueryRaw<T>(string query, Func<DbDataReader, T> map)
        {
            if (this._database == null)
                await Task.FromException<NotSupportedException>(new NotSupportedException("Mongodb not support."));

            List<T> entities = new List<T>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                dbContext.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                        entities.Add(map(result));
                }
                dbContext.Database.CloseConnection();
            }
            return await Task.FromResult(entities);
        }

        public async Task<IEnumerable<T>> SQLQueryRaw<T>(string procedureName, Action<DbParameterCollection>? StoreParametersAction = null)
        {
            List<T>? list = null;
            if (this._database == null)
                await Task.FromException<NotSupportedException>(new NotSupportedException("Mongodb not support."));

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;

                if (StoreParametersAction != null)
                    StoreParametersAction.Invoke(command.Parameters);
                dbContext.Database.OpenConnection();

                using (IDataReader reader = command.ExecuteReader())
                    list = reader.AutoMap<T>();

                dbContext.Database.CloseConnection();
            }
            return await Task.FromResult(list);
        }

        public async Task<DataTable> SQLGetDataTable(string sql, string tableName = "Table1")
        {
            if (this._database == null)
                await Task.FromException<NotSupportedException>(new NotSupportedException("Mongodb not support."));

            var dbFatory = DbProviderFactories.GetFactory(dbContext.Database.GetDbConnection());
            using (var dad = dbFatory.CreateDataAdapter())
            {
                dad.SelectCommand = dbContext.Database.GetDbConnection().CreateCommand();
                dad.SelectCommand.CommandText = sql;
                dad.TableMappings.Add("Table", tableName);
                var cmd = dbFatory.CreateCommandBuilder();
                cmd.DataAdapter = dad;

                DataSet ds = new DataSet();
                dad.Fill(ds);
                return ds.Tables[0];
            }
        }

        public async Task<DataRow?> GetDataRow(string sql, string tableName = "Table1")
        {
            var dt = await this.SQLGetDataTable(sql, tableName);
            return await Task.FromResult(dt.Rows.Count == 0 ? null : dt.Rows[0]);
        }
        public async Task SQLUpdateDataTable(DataTable dtTable)
        {
            var dbFatory = DbProviderFactories.GetFactory(dbContext.Database.GetDbConnection());

            using (var dad = dbFatory.CreateDataAdapter())
            {
                dad.SelectCommand = dbContext.Database.GetDbConnection().CreateCommand();
                dad.SelectCommand.CommandText = $"SELECT * FROM " + dtTable.TableName + " WHERE 1<>1";
                dad.TableMappings.Add("Table", dtTable.TableName);
                var cmd = dbFatory.CreateCommandBuilder();
                cmd.DataAdapter = dad;
                dad.Update(dtTable);
            }
            await Task.CompletedTask;
        }

        private IFindFluent<T, T> MongoQuery<T>(Expression<Func<T, bool>> filter, string? orderBy) where T : class
        {
            IFindFluent<T, T>? result = null;

            if (filter == null)
                result = this._database.GetCollection<T>(GetCollectionName<T>()).Find(Builders<T>.Filter.Empty);
            else
                result = this._database.GetCollection<T>(GetCollectionName<T>()).Find(filter);

            if (!string.IsNullOrEmpty(orderBy))
                result = GetMongoOrderby(result, orderBy);

            return result;
        }

        private IQueryable<T> DbContextQuery<T>(Expression<Func<T, bool>> filter, string? orderBy = null, params Expression<Func<T, object>>[] childIncludes) where T : class
        {
            var query = dbContext.Set<T>().AsQueryable();
            var result = childIncludes
                        .Aggregate(
                            query.AsQueryable(),
                            (current, include) => current.Include(include)
                        );
            if (filter != null)
                result = result.Where(filter);

            if (!string.IsNullOrEmpty(orderBy))
                result = GetOrderby(result, orderBy);

            return result;
        }

        private IOrderedQueryable<T> GetOrderby<T>(IQueryable<T> Query, string orderBy) where T : class
        {
            IOrderedQueryable<T>? orderQuery = null;
            var orderFields = orderBy.Split(",");
            var isFirst = true;
            foreach (string orderField in orderFields)
            {
                var tmp = orderField.Split(" ");
                string fieldSort = tmp[0];
                string sortDirection = "ASC";

                if (tmp.Length > 1)
                    sortDirection = tmp[1].ToUpper();

                if (isFirst)
                {
                    if (sortDirection == "ASC")
                        orderQuery = Query.OrderBy(DynamicExpressionParser.ParseLambda<T, string>(ParsingConfig.Default, false, fieldSort, new object[0]));
                    else
                        orderQuery = Query.OrderByDescending(DynamicExpressionParser.ParseLambda<T, string>(ParsingConfig.Default, false, fieldSort, new object[0]));

                    isFirst = false;
                }
                else
                {
                    if (sortDirection == "ASC")
                        orderQuery = orderQuery.ThenBy(DynamicExpressionParser.ParseLambda<T, string>(ParsingConfig.Default, false, fieldSort, new object[0]));
                    else
                        orderQuery = orderQuery.ThenByDescending(DynamicExpressionParser.ParseLambda<T, string>(ParsingConfig.Default, false, fieldSort, new object[0]));
                }
            }
            return orderQuery;
        }

        private IFindFluent<T, T> GetMongoOrderby<T>(IFindFluent<T, T> Query, string orderBy) where T : class
        {
            SortDefinitionBuilder<T> builder = Builders<T>.Sort;
            SortDefinition<T>? sortDifination = null;

            var orderFields = orderBy.Split(",");
            var isFirst = true;
            foreach (string orderField in orderFields)
            {
                var tmp = orderField.Split(" ");
                string fieldSort = tmp[0];
                string sortDirection = "ASC";

                if (tmp.Length > 1)
                    sortDirection = tmp[1].ToUpper();

                if (isFirst)
                {
                    if (sortDirection == "ASC")
                        sortDifination = builder.Ascending(fieldSort);
                    else
                        sortDifination = builder.Descending(fieldSort);

                    isFirst = false;
                }
                else
                {
                    if (sortDirection == "ASC")
                        sortDifination = builder.Ascending(fieldSort);
                    else
                        sortDifination = builder.Descending(fieldSort);
                }
            }
            return Query.Sort(sortDifination);
        }

        private IFindFluent<T, T> GetMongoPage<T>(IFindFluent<T, T> query, Pagination pagination, int pageIndex) where T : class
        {
            if (pageIndex < 1 || pageIndex > pagination.TotalPage)
            {
                throw new ArgumentOutOfRangeException(null,
                $"*** Page index must >= 1 and =< {pagination.TotalPage}! * **");
            }

            return query
                .Skip(GetSkip(pageIndex, pagination.PageSize))
                .Limit(pagination.PageSize);
        }

        private IQueryable<T> GetDbContextPage<T>(IQueryable<T> query, Pagination pagination, int pageIndex) where T : class
        {
            if (pageIndex < 1 || pageIndex > pagination.TotalPage)
            {
                throw new ArgumentOutOfRangeException(null,
                $"*** Page index must >= 1 and =< {pagination.TotalPage}! * **");
            }

            return query
                .Skip(GetSkip(pageIndex, pagination.PageSize))
                .Take(pagination.PageSize);
        }
    }
}
