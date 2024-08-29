using Abp_BeTech.CategoryDato;
using Abp_BeTech.Categorys;
using Abp_BeTech.Specifications;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Abp_BeTech.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class CategoryController : ControllerBase
    {
  

            private readonly ICategoryAppService _categoryAppService;

            public CategoryController(ICategoryAppService categoryAppService)
            {
                _categoryAppService = categoryAppService;
            }



        [HttpPost]
        public async Task<IActionResult> CreateCategorySpecification(CategorySpecificationsDtos data)
        {
            
            var result = await _categoryAppService.CreateCategory(data.Category, data.SpecificationsDtos);
            if (result.IsSuccess)
            {
                return Ok(result.Entity);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategorySpecificationsDtos data)
        {
            var result = await _categoryAppService.UpdateCategory(data.Category, data.SpecificationsDtos);
            return Ok(result);
        }

        
    }
    }



