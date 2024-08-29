using Abp_BeTech.Models;
using Abp_BeTech.ReviwResult;
using Abp_BeTech.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.Specifications
{
    public interface ISpecificationsAppService
    {
        Task<PagedResultDto<SpecificationsDto>> GetListAsync(SpecificationsListDto input);
        Task<SpecificationsDto> CreateAsync(SpecificationsDto input);
        Task<ResultView<SpecificationsDto>> UpdateAsync(SpecificationsDto input);
        Task<SpecificationsDto> DeleteAsync(SpecificationsDto input);
        Task<string>GetByNameAsync(string name);
        Task<List< SpecificationsDto>> GetSpecificationsByCategory(int CategoryId);


    }
}
