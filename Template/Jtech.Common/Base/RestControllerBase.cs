using Esprima.Ast;
using Jtech.Common.BusinessLogic;
using Jtech.Common.DataStore.Interface;
using Jtech.Common.DataStore.Paging;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using System.Linq.Dynamic.Core;

namespace Jtech.Common.Base
{

    public abstract class RestControllerBase<TModel,TDtoCreate,TDtoUpdate, TDtoGet> :JtechControllerBase  where TModel:class 
    {
        public const int ERR_VALIDATE_STATUS_CODE = 412;

        private readonly CRUDLogic _logic;

        protected CRUDLogic Logic
        {
            get
            {
                return this._logic;
            }
        }

        protected RestControllerBase(IServiceProvider provider, CRUDLogic logic) : base(provider)
        {
            this._logic = logic;  
        }

        [HttpGet("Paging")]
        public virtual async Task<IActionResult> GetList(string? filter, string? orderBy = null, int pageIndex = 1, int pageSize = 10)
        {
            Pagination page = new Pagination(0, pageSize, pageIndex);

            var result = await this._logic.Search<TModel>(page, filter: filter, orderBy: orderBy);


            var lst = result.Select(x =>this.CopyToObject<TModel,TDtoGet>( x));
            
            return this.Ok(new PageResult<IEnumerable<TDtoGet>>(page, lst));
        }

        //filter example c => c.Id= "65dd8da64a3048ce5ce4d41e"
        [HttpGet]
        public virtual async Task<IActionResult> GetList(string? filter, string? OrdreBy=null)
        {
            var lst =await this._logic.GetList<TModel>(filter, OrdreBy);
            if (lst.Count() == 0)
                return NoContent();

            return Ok(lst.Select(x => this.CopyToObject<TModel, TDtoGet>(x)).ToList());
        }

   
        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody]  TDtoCreate dto) 
        {
            try
            {
                var model = this.CopyToObject<TDtoCreate, TModel>(dto);
                await this._logic.Create<TModel>(model);

                string id = model is IEntity ? ((IEntity)model).Id : "Auto";
                return Created($"/{id}", new { id = id });
            }
            catch (LogicValidateException ex)
            {
                return StatusCode(ERR_VALIDATE_STATUS_CODE, ex);
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update([Required]  TDtoUpdate dto,[Required] string id)
        {
            try
            {
                var model = await this._logic.GetById<TModel>(id);
                if (model == null)
                    return NoContent();
                else
                {
                    this.CopyObject(dto, model);
                    await this._logic.Update<TModel>(model);
                    return this.Ok(dto);
                }
            }
            catch (LogicValidateException ex)
            {
                return StatusCode(ERR_VALIDATE_STATUS_CODE, ex);
            }
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete([Required] string id)
        {
            try
            {
                await this._logic.DeleteById<TModel>(id);
                return await Task.FromResult(NoContent());
            }
            catch (LogicValidateException ex)
            {
                return StatusCode(ERR_VALIDATE_STATUS_CODE, ex);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById([Required] string id)
        {
            var model = await this._logic.GetById<TModel>(id);
            if (model == null)
                return NoContent();

            return this.Ok(this.CopyToObject<TModel,TDtoGet>(model));
        }
    }

}
