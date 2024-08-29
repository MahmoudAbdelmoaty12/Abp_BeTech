
using Abp_BeTech.CategoryDato;
using Abp_BeTech.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Abp_BeTech.Permissions;
using Abp_BeTech.Exceptionsss;
using NUnit.Framework;
using Abp_BeTech.ViewResult;
using Abp_BeTech.ReviwResult;
using Abp_BeTech.Specifications;
using Abp_BeTech.Domain_Service;
using Volo.Abp.ObjectMapping;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Abp_BeTech.CategorySpecifications;
using System;


namespace Abp_BeTech.Categorys
{
    public class CategoryAppService : ApplicationService, ICategoryAppService
    {
        private readonly IRepository<Category, int> categoryRepository;
        readonly CategoryDomainService _categoryDomainService;
        private readonly    IRepository<Specification,int> _specification;
        readonly IRepository<CategorySpecification> _categorySpecification;
        public CategoryAppService(IRepository<Category, int> categoryRepository, CategoryDomainService
            categoryDomainService, IRepository<Specification,int>  specifications,
            IRepository<CategorySpecification,int> categorySpecification
            )
        {
            this.categoryRepository = categoryRepository;
            _categoryDomainService = categoryDomainService;
            _specification = specifications;
            _categorySpecification = categorySpecification;
        }
        [Authorize(Abp_BeTechPermissions.Category.Create)]
        public async Task<ResultView<CategorySpecificationsDtos>> CreateCategory(CategoryDtos category, List<SpecificationsDto> input)
        {
            var cate=await categoryRepository.FindAsync(x=>x.Name==category.Name);

            if (cate == null) {

                var categ = ObjectMapper.Map<CategoryDtos, Category>(category);
                var spec = ObjectMapper.Map<List<SpecificationsDto>, List<Specification>>(input);

                var Specification = await _categoryDomainService.CreateCategorySpecificationAsync(categ, spec);
                var specificatioList = ObjectMapper.Map<List<Specification>, List<SpecificationsDto>>(Specification);
                var category1 = await categoryRepository.GetAsync(x => x.Id == categ.Id);
                var newcategory = ObjectMapper.Map<Category, CategoryDtos>(category1);
                var categorySpecificationDtos = new CategorySpecificationsDtos()

                {
                    Category = newcategory,
                    SpecificationsDtos = specificatioList,
                };
                return new ResultView<CategorySpecificationsDtos>()
                {
                    Entity = categorySpecificationDtos,
                    IsSuccess = true,
                    Message = "Created Successfully"
                };
            }
                
                    return new ResultView<CategorySpecificationsDtos>()
                    {
                        Entity = null,
                        IsSuccess = false,
                        Message = "Category is devind"
                    };
                
            
              
            
            }

        

        [Authorize(Abp_BeTechPermissions.Category.Create)]
        public async Task<ResultView<CategoryDtos>> CreateAsync(CreateUpdateCategoryDto inpout)
        {
            var oldcate = await categoryRepository.FirstOrDefaultAsync(x => x.Name == inpout.Name);
            var olcat1 = ObjectMapper.Map<Category, CategoryDtos>(oldcate);
            if (olcat1 != null)
            {
                return new ResultView<CategoryDtos>
                {
                    Entity = olcat1,
                    IsSuccess = false,
                    Message = "Category Found"
                };
            }

            var category = ObjectMapper.Map<CreateUpdateCategoryDto, Category>(inpout);

            var resulst = await categoryRepository.InsertAsync(category, autoSave: true);
            var createdCategory = ObjectMapper.Map<Category, CategoryDtos>(resulst);
            return new ResultView<CategoryDtos>
            {
                Entity = createdCategory,
                IsSuccess = true,
                Message = "Created Successfully"
            }

                 ;

        }
        public async Task<ResultView<CategoryDtos>> SearchByNameAsync(string Name)
        {
            var category = await categoryRepository.FindAsync(x => x.Name == Name);
            if (category != null)
            {
                var cate1 = ObjectMapper.Map<Category, CategoryDtos>(category);
                return new ResultView<CategoryDtos>()
                {
                    Entity = cate1,
                    IsSuccess = true,
                    Message = "Successfully"
                };
            }
            return new ResultView<CategoryDtos>()
            {
                Entity = null,
                IsSuccess = false,
                Message = "Category Not Found"
            };
        }
        [Authorize(Abp_BeTechPermissions.Category.Delete)]
        public async Task<ResultView<CategoryDtos>> HardDeleteCategory(CategoryDtos category)
        {
            var oldcategory = await categoryRepository.FirstOrDefaultAsync(x => x.Id == category.Id);
            var result = ObjectMapper.Map<Category, CategoryDtos>(oldcategory);
            if (result == null)
            {
                return new ResultView<CategoryDtos>() { Entity = null, IsSuccess = false, Message = "Category Not Found" };
            }
            await categoryRepository.DeleteAsync(result.Id, autoSave: true);

            return new ResultView<CategoryDtos>() { Entity = result, IsSuccess = true, Message = "Successfully" };
        }

        [Authorize(Abp_BeTechPermissions.Category.Delete)]
        public async Task<ResultView<CategoryDtos>> SoftDeleteCategory(CategoryDtos category)
        {
            var oldcategory = await categoryRepository.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (oldcategory == null)
            {
                return new ResultView<CategoryDtos>() { Entity = null, IsSuccess = false, Message = "Category Not Found" };
            }
            oldcategory.IsDeleted = true;
          
            var result = await categoryRepository.UpdateAsync(oldcategory, autoSave: true);
            var newcate = ObjectMapper.Map<Category, CategoryDtos>(result);
            return new ResultView<CategoryDtos>() { Entity = newcate, IsSuccess = true, Message = "Successfully" };
        }
        [Authorize(Abp_BeTechPermissions.Category.Default)]
        public async Task<ResultDataList<SpecificationsDto>> GetSpecificationsByCategoryId(int CategoryId)
        {
            var category=await categoryRepository.GetAsync(x=>x.Id== CategoryId);
            var Specifications = _specification
          .WithDetails()
          .Where(s => s.CategorySpecifications.Any(cs => cs.CategoryId == category.Id))
          .ToList();

            var lisSpec = ObjectMapper.Map<List<Specification>,List<SpecificationsDto>>(Specifications);
            if (category != null)
            {
                return new ResultDataList<SpecificationsDto>()
                {

                    Entities = lisSpec,
                    Count = lisSpec.Count,
                };
             
            }
            return new ResultDataList<SpecificationsDto> { Entities = null, Count = lisSpec.Count };

        }

        public async Task<ResultDataList<CategoryDtos>> GetAllCategory()
        {
            var categories = await categoryRepository.GetListAsync();
            var categoryDtos = ObjectMapper.Map<List<Category>, List<CategoryDtos>>(categories);

            return new ResultDataList<CategoryDtos>
            {

                Entities = categoryDtos,
                Count = categories.Count

            };
        }
        [Authorize(Abp_BeTechPermissions.Category.Edit)]
        public async Task<ResultView<CategorySpecificationsDtos>> UpdateCategory(CategoryDtos updatedCategory, List<SpecificationsDto> specificationsDtos)
        {
            try
            {

                var categoryEntity = await categoryRepository.GetAsync(x => x.Id == updatedCategory.Id);

                categoryEntity.Name = updatedCategory.Name;
                var category1 = await categoryRepository.UpdateAsync(categoryEntity, autoSave: true);





                var lisSpecifications = ObjectMapper.Map<List<SpecificationsDto>, List<Specification>>(specificationsDtos);

                 await _categoryDomainService.UpdateCategory(lisSpecifications);
                var Specifications = _categorySpecification
                     .WithDetails(cs => cs.Specification)
                    .Where(cs => cs.CategoryId == category1.Id)
                   .Select(cs => cs.Specification)
                    .ToList();

                var categroy = ObjectMapper.Map<Category, CategoryDtos>(category1);
                var newSpecifications = ObjectMapper.Map<List<Specification>, List<SpecificationsDto>>(Specifications);
                var categorySpecification = new CategorySpecificationsDtos()
                {
                    Category = categroy,
                    SpecificationsDtos = newSpecifications
                };
                return new ResultView<CategorySpecificationsDtos>
                {
                    Entity = categorySpecification,
                    IsSuccess = true,
                    Message = "Update CategorySpecification successfully"

                };
            }
            catch (Exception)
            {
                return new ResultView<CategorySpecificationsDtos>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message ="Category Not Found"
                };
            }









            
        }
        [Authorize(Abp_BeTechPermissions.Category.Delete)]
        public async Task<ResultView<CategorySpecificationsDtos>> DeleteSpecFromCategory(int CategoryId, int SpecID)
        {
            var categorySpec = await _categorySpecification.FirstOrDefaultAsync(cs =>
                cs.CategoryId == CategoryId && cs.SpecificationId == SpecID);

            if (categorySpec != null)
            {
                await _categorySpecification.DeleteAsync(categorySpec,autoSave:true);
                await _specification.DeleteAsync(SpecID,autoSave:true);




                var category = await categoryRepository.GetAsync(CategoryId);

                var specifications = _categorySpecification
    .WithDetails(cs => cs.Specification)
    .Where(cs => cs.CategoryId == category.Id)
    .Select(cs => cs.Specification)
    .ToList();
                var cate = ObjectMapper.Map<Category, CategoryDtos>(category);
                var listSpec = ObjectMapper.Map<List<Specification>, List<SpecificationsDto>>(specifications);
                var categorySpecification = new CategorySpecificationsDtos()
                {
                    Category = cate,
                    SpecificationsDtos = listSpec
                };


                return new ResultView<CategorySpecificationsDtos>()
                {
                    IsSuccess = true,
                    Entity = categorySpecification,
                    Message = "Specification removed from category successfully."
                };
            }
            return new ResultView<CategorySpecificationsDtos>()
            {
                IsSuccess = false,
                Entity = null,
                Message = "category Not Found"
            };
        }
        [Authorize(Abp_BeTechPermissions.Category.Default)]
        public async Task<ResultView<CategorySpecificationsDtos>> AddSpecToCategory(int CategoryId, SpecificationsDto specificationsDto)
        {

            try
            {
                var category = await categoryRepository.GetAsync(x => x.Id == CategoryId);





                var spec = ObjectMapper.Map<SpecificationsDto, Specification>(specificationsDto);
                await _specification.InsertAsync(spec, autoSave: true);
                var categorySpec = new CategorySpecification
                {
                    Category = category,
                    Specification = spec
                };
                await _categorySpecification.InsertAsync(categorySpec, autoSave: true);


                var specifications = _categorySpecification
    .WithDetails(cs => cs.Specification)
    .Where(cs => cs.CategoryId == category.Id
    )
    .Select(cs => cs.Specification)
    .ToList();
                var cate = ObjectMapper.Map<Category, CategoryDtos>(category);
                var listSpec = ObjectMapper.Map<List<Specification>, List<SpecificationsDto>>(specifications);
                var categorySpecification = new CategorySpecificationsDtos()
                {
                    Category = cate,
                    SpecificationsDtos = listSpec
                };

                return new ResultView<CategorySpecificationsDtos>
                {
                    IsSuccess = true,
                    Entity = categorySpecification,
                    Message = "Specification added to category successfully."
                };
            }catch(Exception x)
            {
                return new ResultView<CategorySpecificationsDtos>()
                {
                    IsSuccess = false,
                    Entity = null,
                    Message = "Category Not Found"
                };
            }
        }

    }
   

}