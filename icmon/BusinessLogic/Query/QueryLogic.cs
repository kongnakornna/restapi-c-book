using Jtech.Common.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp;
using System.Data;
using Helpers = Jtech.Common.Helpers;
using Newtonsoft.Json.Linq;
using MongoDB.Driver;
using System.Data.Odbc;
using System.Text.Json;
using Jtech.Common.DataStore.Paging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Linq.Dynamic.Core;

namespace Jtech.Common.BusinessLogic.Query
{
    public class QueryLogic : BusinessLogicBase
    {
        private const string PLACE_HOLDER_QUERY = "{query}";
        private const string PLACE_HOLDER_FILTER = "{filter}";
        private const string PLACE_HOLDER_ORDER_BY = "{orderby}";
        private const string PLACE_HOLDER_LIMIT = "{limit}";
        private const string PLACE_HOLDER_OFFSET = "{offset}";
        private const string PLACE_HOLDER_RECORD_FROM = "{rec_from}";
        private const string PLACE_HOLDER_RECORD_TO = "{rec_to}";

        private Store _store;
        public QueryLogic(IServiceProvider provider) : base(provider)
        {
            this._store = this.Services.GetService<Store<QueryLogic>>();
        }

        private Query? GetQueryItem(string QueryName)
        { 
            return this._store.GetByCondition<Query>(x => x.QueryName.ToLower() == QueryName.ToLower(), x => x.Connection).Result?.FirstOrDefault();
        }

        private string ReplaceHolder(string cmd,string? filter, string? orderBy)
        {
            if (filter == null)
                cmd = cmd.Replace(PLACE_HOLDER_FILTER, "");
            else
            {
                if (!cmd.Contains(" where "))
                    filter = " where " + filter;
                else
                    filter = " And " + filter;

                cmd = cmd.Replace(PLACE_HOLDER_FILTER, filter);
            }

            if (orderBy == null)
                cmd = cmd.Replace(PLACE_HOLDER_ORDER_BY, "");
            else
            {
                if (!cmd.Contains("order by "))
                    orderBy = " order by " + orderBy;

                cmd = cmd.Replace(PLACE_HOLDER_ORDER_BY, orderBy);
            }
            return cmd;
        }
        
        private string buildCmdCount(string cmd)
        {
            return $"select count(*) from ({cmd}) a";
        }

        private string buildCmdPaging(Pagination page,string cmd,string? cmdPagingTemplate=null)
        {
            string pagingCmd ;
            if (cmdPagingTemplate == null)
                pagingCmd = $"select * from ({cmd}) limit {page.PageSize} offset {page.RecordFrom}";
            else
            {
                pagingCmd = cmdPagingTemplate.Replace(PLACE_HOLDER_QUERY, cmd);

                pagingCmd = pagingCmd.Replace(PLACE_HOLDER_LIMIT, page.PageSize.ToString());
                pagingCmd = pagingCmd.Replace(PLACE_HOLDER_OFFSET, page.RecordFrom.ToString());
                pagingCmd = pagingCmd.Replace(PLACE_HOLDER_RECORD_FROM, page.RecordFrom.ToString());
                pagingCmd = pagingCmd.Replace(PLACE_HOLDER_RECORD_TO, page.RecordTo.ToString());
            }

            return pagingCmd;
        }

        public DataTable SearchBreakByPage(Pagination page, string QueryName, string filter, string? orderBy)
        {
            if (page == null)
                throw new Exception($"{nameof(Pagination)} is null.");

            var query = this.GetQueryItem(QueryName);
            if (query == null)
                throw new ArgumentException($"{QueryName} Not found.");

            var mainCmd = this.ReplaceHolder(query.QueryCommand, filter, orderBy);
            var countCmd = this.buildCmdCount(mainCmd);

            //Pagination page;

            //Get Total count
            using (var conn = new OdbcConnection(query.Connection.ConnectionString))
            {
                try
                {
                    conn.Open();
                    var cmd = new OdbcCommand(countCmd, conn);
                    var total = cmd.ExecuteScalar();
                    page.TotalItems = Convert.ToInt64(total);

                    //page = new Pagination(Convert.ToInt64(total), pageSize, pageIndex);
                }
                finally
                {
                    conn.Close();
                }
            }

            var cmdPage = this.buildCmdPaging(page, mainCmd, query.Connection.PagingCommandTemplate);


            OdbcDataAdapter dad = new OdbcDataAdapter(cmdPage, query.Connection.ConnectionString);
            DataSet ds = new DataSet();
            dad.Fill(ds);

            return ds.Tables[0];
        }

        public DataTable Search(string QueryName,string filter, string? orderBy)
        {
            var query = this.GetQueryItem(QueryName);
            if (query == null)
                throw new ArgumentException($"{QueryName} Not found.");

            var cmd =this.ReplaceHolder(query.QueryCommand,filter,orderBy);
            
            OdbcDataAdapter dad = new OdbcDataAdapter(cmd, query.Connection.ConnectionString);
            DataSet ds = new DataSet();
            dad.Fill(ds);

            return ds.Tables[0];
        }
    }
}
