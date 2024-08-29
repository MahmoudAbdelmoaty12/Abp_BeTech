
using Abp_BeTech.Models;
using Abp_BeTech.Ptoduct;
using Abp_BeTech.PtoductDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        // Endpoint to get all brands
        //[HttpGet("brands")]
        //public async Task<ActionResult<List<string>>> GetAllBrands()
        //{
        //    var brands = await _productAppService.GetAllBrands();
        //    return Ok(brands);
        //}

        //// Endpoint to get brands filtered by category ID
        //[HttpGet("brands/{categoryId}")]
        //public async Task<ActionResult<List<string>>> GetBrands(int categoryId)
        //{
        //    var brands = await _productAppService.GetBrands(categoryId);
        //    return Ok(brands);
        //}


        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ProductWithSpecificationsDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Get the productCategorySpecificationDtos from the form
                var productSpecificationsJson = Request.Form["productCategorySpecificatoinDto"];
                productSpecificationsJson = "[" + productSpecificationsJson + "]";
                // Deserialize the JSON into a list of ProductCategorySpecificationDto
                var productCategorySpecificatoinDtoList = JsonConvert.DeserializeObject<List<ProductCategorySpecificatoinDto>>(productSpecificationsJson);

                // Assign the deserialized list to the productDto
                productDto.productCategorySpecificatoinDto = productCategorySpecificatoinDtoList;
            }catch (Exception ex)
            {
                throw new Exception( ex.Message);
            }
            var result = await _productAppService.Create(productDto);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromForm] ProductWithSpecificationsDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Get the productCategorySpecificationDtos from the form
                var productSpecificationsJson = Request.Form["productCategorySpecificatoinDto"];
                productSpecificationsJson = "[" + productSpecificationsJson + "]";
                // Deserialize the JSON into a list of ProductCategorySpecificationDto
                var productCategorySpecificatoinDtoList = JsonConvert.DeserializeObject<List<ProductCategorySpecificatoinDto>>(productSpecificationsJson);

                // Assign the deserialized list to the productDto
                productDto.productCategorySpecificatoinDto = productCategorySpecificatoinDtoList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            var result = await _productAppService.Update(productDto);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<List<string>>> FilterProducts([FromBody]FiltterProductDto filterProductsDto, int categoryId, int ItemsPerPage, int PageNumbe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brands = await _productAppService.FilterProducts(filterProductsDto, categoryId, ItemsPerPage, PageNumbe);



            return Ok(brands);
        }
        //[HttpGet]
        //public async Task<List<string>> GetBrands(int categoryid)
        //{
        //    var products = await _productAppService.GetBrands( categoryid);
        //    var productNames = new List<string>();

        //    foreach (var product in products)
        //    {
        //        productNames.Add(product);
        //    }

        //    return productNames;
        //}


        //public async Task<ActionResult<List<string>>> GetAllBrands()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var brands = await _productAppService.GetAllBrands();
        //    return Ok(brands);
        //}
        //[HttpGet]
        //public async Task<List<string>> GetAllBrands()
        //{
        //    var products = await _productAppService.GetAllBrands(new PagedAndSortedResultRequestDto());
        //    var productNames = new List<string>();

        //    foreach (var product in products)
        //    {
        //        productNames.Add(product);
        //    }

        //    return productNames;
        //}
        //[HttpPost]
        //public async Task<IActionResult> Productssss([FromForm] ProductWithSpecificationsDto productDto)
        //{
        //    // Parse the complex data (productCategorySpecificatoinDtos)
        //    var productSpecificationsJson = Request.Form["productCategorySpecificatoinDto"];
        //    if (!string.IsNullOrEmpty(productSpecificationsJson))
        //    {
        //        try
        //        {
        //            var productCategorySpecificatoinDto = JsonConvert.DeserializeObject<ProductCategorySpecificatoinDto>(productSpecificationsJson);
        //            productDto.productCategorySpecificatoinDto = new List<ProductCategorySpecificatoinDto> { productCategorySpecificatoinDto };
        //        }
        //        catch (JsonSerializationException ex)
        //        {
        //            Console.WriteLine("Error during deserialization: " + ex.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Unexpected error: " + ex.Message);
        //        }
        //    }
        //    return Ok(productDto);
        //}

        //[HttpPost]
        //[Route("create-with-specifications")]
        //public async Task<IActionResult> CreateProductWithSpecifications([ModelBinder(BinderType = typeof(ProductWithSpecificationsDtoModelBinder))] ProductWithSpecificationsDto input)
        //{
        //    var result = await _productAppService.Create(input);
        //    return Ok(result);
        //}




    }

}
