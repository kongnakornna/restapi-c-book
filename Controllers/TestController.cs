using Jtech.Common.Base;
using Jtech.Common.BusinessLogic;
using Jtech.Common.BusinessLogic.Query;
using Jtech.Common.DataStore;
using Jtech.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Extensions;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using TestCommon.Models;

namespace TestCommon.Controllers
{
    public class TestController : JtechControllerBase
    {
        private Store? _store;
        private IServiceProvider _provider;

        public TestController(IServiceProvider provider) : base(provider)
        {
            _store=provider.GetService<Store>();
            _provider=provider;
        }

        [HttpPost("test")]
        public async Task<IActionResult> test(Category model)
        {
            var cat = new Category { Name = "TEST" };
                await _store.Create(cat);
             
            return Created(cat.Id,new {id= cat.Id });
        }

        [HttpGet("TestDI")]
        public IActionResult testDI(string id)
        {
            //var di1 = this._provider.GetService<ClientResolve>();
            //var di2 = this._provider.GetService<ClientResolve2>();

            //di1.Write();
            //di2.Write();
            var logic=this._provider.GetService<CRUDLogic>();
            return this.Ok(logic.GetById<Book>(id).Result);
        }

        [HttpGet("GetBlog")]
        public IActionResult GetBlog(string id)
        {
            
            Blog b = new Blog();
           var ll=this._provider.GetService<DBLogLogic>();
            ll.StampLog<Blog>(b,true);

            var inj = new TestInject();
            inj.message = "Test Inject";
            RunningLogic logic = ActivatorUtilities.CreateInstance<RunningLogic>(this._provider,new object[] { inj,});
            return this.Ok(logic.GetBlog(id));


        }

       


    }
}
