using Abp_BeTech.PtoductDto;
using Abp_BeTech.ReviwResult;
using Abp_BeTech.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp_BeTech.Ptoduct
{
    public interface IProductAppService:IApplicationService
    {
        //  Task<List<string>> GetBrands(int categoryid);
        //   Task<List<string>> GetAllBrands();
        //Task<GetAllProductsDtos>GetAllProductsAsync();
        Task<ResultView<ProductCategorySpecificationsListDto>> Create(ProductWithSpecificationsDto productDto);
        Task<ResultView<GetProductSpecificationNameValueDtos>> GetOne(int id);
        Task<ResultView<ProductCategorySpecificationsListDto>> SoftDelete(int productId);

        Task<ResultView<ProductCategorySpecificationsListDto>> HardDelete(int productId);
        Task<ResultDataList<GetAllProductsDtos>> SortProductsByDesending(int categoryId, int ItemsPerPage, int PageNumber);
        Task<ResultDataList<GetAllProductsDtos>> SortProductsByAscending(int categoryId, int ItemsPerPage, int PageNumber);
        Task<ResultDataList<GetAllProductsDtos>> GetAllPagination(int ItemsPerPage, int PageNumber);
        Task<ResultView<ProductWithSpecificationsDto>> Update(ProductWithSpecificationsDto productDto);
        Task<ResultDataList<GetAllProductsDtos>> SearchProduct(string ModelName, int ItemsPerPage, int PageNumber);
        Task<ResultDataList<GetAllProductsDtos>> FilterProductsByCategory(int categoryId, int ItemsPerPage, int PageNumber);
        Task<ResultDataList<GetAllProductsDtos>> FilterProducts(FiltterProductDto fillterProductsDto, int categoryId, int ItemsPerPage, int PageNumber);
       Task<List<string>> GetBrandsProduct( int categoryid);
        Task<List<string>> GetAllBrands();
        Task<ResultDataList<GetAllProductsDtos>> FilterDiscountedProducts();
        Task<ResultDataList<GetAllProductsDtos>> FilterNewlyAddedProducts(int count);
    }
}
