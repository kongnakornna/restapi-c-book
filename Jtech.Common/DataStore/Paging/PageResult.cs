using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.DataStore.Paging
{
    public class PageResult<T> : Pagination,IActionResult 
    {
        public PageResult(Pagination page,T Results) : base(page.TotalItems, page.PageSize,page.Current)
        {
            this.Results = Results;
        }

        public T Results { get; init; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this);
            await result.ExecuteResultAsync(context);
        }
    }
}
