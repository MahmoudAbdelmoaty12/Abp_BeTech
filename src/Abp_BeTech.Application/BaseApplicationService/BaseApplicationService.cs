using Abp_BeTech.Categorys;
using Abp_BeTech.Models;
using Abp_BeTech.ReviewDto;
using Abp_BeTech.Reviews;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Abp_BeTech.BaseApplicationService
{
    public class BaseApplicationService<TEntity, TKey> :ApplicationService
      where TEntity : class, IEntity<TKey>
    {
        private readonly IRepository<TEntity, TKey> _repository;

        public BaseApplicationService(IRepository<TEntity, TKey> repository)
        {
            _repository = repository;
        }

   
        public async Task<TEntity> GetAsync(TKey id)
        {
            return await _repository.GetAsync(id);
        }
     public async   Task<TEntity> GetByNameAsync(TKey Name)
        {
            return await _repository.GetAsync(Name);
        }
        public async Task<PagedResultDto<TEntity>> GetListAsync(GetCategoryListDto input)
        {
          
            var totalCount = await _repository.GetCountAsync();

           
            var entities = await _repository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting 
            );

           
            return new PagedResultDto<TEntity>(
                totalCount,
                entities
            );
        }


        public async Task CreateAsync(TEntity entity)
        {
            await _repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TKey id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}

