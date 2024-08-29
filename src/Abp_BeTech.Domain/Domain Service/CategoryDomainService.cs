using Abp_BeTech.Models;
using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.ObjectMapping;

namespace Abp_BeTech.Domain_Service
{
    public class CategoryDomainService : DomainService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Specification> _specificationRepository;
        readonly IRepository<CategorySpecification> _categorySpecification;
        public CategoryDomainService(IRepository<Category> categoryRepository, IRepository<Specification> 
            
            specificationRepository ,IRepository<CategorySpecification> CategorySpecification)
        {
            _categoryRepository = categoryRepository;
            _specificationRepository = specificationRepository;
            _categorySpecification = CategorySpecification;
        }

        public async Task<List<Specification>> CreateCategorySpecificationAsync(Category category, List<Specification> specifications)
        {
            // Save the category if it's new
            if (category.Id !=null)
            {
                category = await _categoryRepository.InsertAsync(category, autoSave: true);
            }
          
            foreach (var specification in specifications)
            {
                if(specification!=null)
                {
                  var newSpecification=await _specificationRepository.InsertAsync(specification,autoSave:true);
                   
                    var categorySepcification =await _categorySpecification.InsertAsync(new CategorySpecification()
                    {
                        CategoryId = category.Id,
                        Specification = newSpecification
                    },autoSave:true);
                 
                }
                else
                {
                    throw new UserFriendlyException("Category not found.");
                }
             
            }
            //  var Specifications =  _specificationRepository
            //.WithDetails()
            //.Where(s => s.CategorySpecifications.Any(cs => cs.CategoryId == category.Id))
            //.ToList();
            var listspecifications = _categorySpecification
 .WithDetails(cs => cs.Specification)
 .Where(cs => cs.CategoryId == category.Id)
 .Select(cs => cs.Specification)
 .ToList();
            return listspecifications;
        }

        public async Task  UpdateCategory(List<Specification> specificationsDtos)
        {
            


            foreach (var spec in specificationsDtos)
            {
                var specification = await _specificationRepository.GetAsync(x => x.Id == spec.Id);
                specification.Name = spec.Name;
              var speca=  await _specificationRepository.UpdateAsync(specification, autoSave: true);
              
              
           
            }
       

           

           
          
        }






    }

}
