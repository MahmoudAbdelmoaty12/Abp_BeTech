using Abp_BeTech.CategoryDato;
using Abp_BeTech.ReviwResult;
using Abp_BeTech.Specifications;
using Abp_BeTech.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp_BeTech.Categorys
{
    public interface ICategoryAppService
    {
        //  Task<PagedResultDto<CategoryDtos>> GetListAsync(GetCategoryListDto getProductListDto);
        //  Task<ResultView <CategoryDtos>> GetCategoryById(int id);
          Task<ResultView< CategoryDtos>> SearchByNameAsync(string Name);
          Task<ResultView<CategoryDtos>> CreateAsync(CreateUpdateCategoryDto input);
        //  Task< ResultView<CategoryDtos>> UpdateAsync(CreateUpdateCategoryDto input);
   
         Task<ResultView<CategorySpecificationsDtos>> CreateCategory(CategoryDtos category,List<SpecificationsDto> input);
          Task<ResultView<CategoryDtos>> HardDeleteCategory(CategoryDtos category);
          Task<ResultView<CategoryDtos>> SoftDeleteCategory(CategoryDtos category);
            Task<ResultDataList<CategoryDtos>> GetAllCategory();
         //   Task<CategorySpecificationDto> GetCategoryById(int id);
            Task<ResultView<CategorySpecificationsDtos>> UpdateCategory(CategoryDtos updatedcategory, List<SpecificationsDto> specificationsDtos);
            Task<ResultView<CategorySpecificationsDtos>> DeleteSpecFromCategory(int CategoryId, int SpecID);
            Task<ResultView<CategorySpecificationsDtos>> AddSpecToCategory(int CategoryId, SpecificationsDto specificationsDto);
            Task<ResultDataList<SpecificationsDto>> GetSpecificationsByCategoryId(int CategoryId);
        

    }
}
