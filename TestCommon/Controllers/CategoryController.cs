using Jtech.Common.Base;
using Jtech.Common.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TestCommon.Models;
using Helpers = Jtech.Common.Helpers;

namespace TestCommon.Controllers
{
    public class CategoryDTO {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? CategoryGroup { get; set; }
    };
    public record CategoryCreateDTO([Required]string Name, string? CategoryGroup);

    public record CategoryUpdateDTO( [Required] string Name, string? CategoryGroup);

    public class CategoryController : RestControllerBase<Category, CategoryCreateDTO, CategoryUpdateDTO, CategoryDTO>
    {
       
        public CategoryController(IServiceProvider provider, CRUDLogic logic) : base(provider, logic)
        {
          
        }
    }



}
