using Abp_BeTech.Categorys;
using Abp_BeTech.ReviewDto;
using Abp_BeTech.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.Reviews
{
    public interface IReviewAppService
    {
        Task<PagedResultDto<ReviewDtos>>GetListAsync(GetReviewListDtos input );
        Task<ReviewDtos> CreateAsync(CreateUpdateReviewDto input);
        Task<ResultView< ReviewDtos>> UpdateAsync(CreateUpdateReviewDto input);
        Task<bool> DeleteAsync(int id);
    }
}
