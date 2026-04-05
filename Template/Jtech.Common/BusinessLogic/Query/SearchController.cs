using Jtech.Common.Base;
using Jtech.Common.DataStore;
using Jtech.Common.DataStore.Paging;
using Jtech.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jtech.Common.BusinessLogic.Query
{

    [ApiController]
    [Route("/api/v1/[controller]")]
    public class SearchController : JtechControllerBase
    {
        private readonly QueryLogic _logic;

        public SearchController(IServiceProvider provider) : base(provider)
        {
            _logic = Services.GetService<QueryLogic>();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Search(string name,string? filter,string? orderBy)
        {
            var result = _logic.Search(name, filter, orderBy);
            return await Task.FromResult(
                this.Ok(Helpers.Json.Convert(result)));
        }

        [HttpGet("{name}/paging")]
        public async Task<IActionResult> SearchBreakByPaging(string name, string? filter, string? orderBy,int pageIndex=1,int pageSize=10)
        {
            Pagination page = new Pagination(0, 10, 1);
            var dt = _logic.SearchBreakByPage(page, name, filter, orderBy);

            var json = Helpers.Json.Serialize(dt);
            return await Task.FromResult(new PageResult<JsonDocument>(page, JsonDocument.Parse(json)));
        }
    }
}
