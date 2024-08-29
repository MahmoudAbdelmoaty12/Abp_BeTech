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

namespace Abp_BeTech.Domain_Service
{
    public class CategorySpecificationDomainService : DomainService
    {
        private readonly IRepository<CategorySpecification> _categorySpecificationRepository;

        public CategorySpecificationDomainService(IRepository<CategorySpecification> categorySpecificationRepository)
        {
            _categorySpecificationRepository = categorySpecificationRepository;
        }


        public async Task<CategorySpecification> GetSpeByCategoryAndSpecId(int CategoryId, int SpecificationId)
        {
            var categorySpecification = await _categorySpecificationRepository.FindAsync(x=>x.CategoryId==CategoryId&
            x.SpecificationId==SpecificationId
            );
          


            if (categorySpecification == null)
            {
                throw new Exception("Specification Not Found");
            }


            return categorySpecification;

        }










        public async Task<CategorySpecification> DeleteAsync(CategorySpecification input)
        {

            var categorySpecification = await _categorySpecificationRepository.FirstOrDefaultAsync(
                x => x.CategoryId == input.CategoryId && x.SpecificationId == input.SpecificationId
            );

            if (categorySpecification == null)
            {
                throw new UserFriendlyException("CategorySpecification not found.");
            }

      
            await _categorySpecificationRepository.DeleteAsync(categorySpecification ,autoSave:true);

            return categorySpecification;
        }
    }

}
