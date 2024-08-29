using Abp_BeTech.CategoryDato;
using Abp_BeTech.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp_BeTech.BaseApplicationService
{
    public interface IBaseApplicationService<TEntity,TKey>:IApplicationService where TEntity : class
    {
        Task<PagedResultDto<TEntity>> GetListAsync(TEntity input);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetByNameAsync(TKey Name);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
