using Abp_BeTech.CategoryDato;
using Abp_BeTech.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Polly;
using System.Linq.Dynamic.Core;
using static Abp_BeTech.Permissions.Abp_BeTechPermissions;
using Specification = Abp_BeTech.Models.Specification;
using Microsoft.AspNetCore.Authorization;
using Abp_BeTech.Permissions;
using Abp_BeTech.Exceptions;
using Abp_BeTech.ViewResult;
namespace Abp_BeTech.Specifications
{
    public class SpecificationsAppService : ApplicationService, ISpecificationsAppService
    {
        readonly IRepository<Specification,int> specificationRepository;
        public SpecificationsAppService(IRepository<Specification, int> specificationRepository)
        {
        
        this.specificationRepository = specificationRepository;
        }

     //  [Authorize(Abp_BeTechPermissions.Todo.Create)]

        public async Task<SpecificationsDto> CreateAsync(SpecificationsDto input)
        {

        
            var specifications=ObjectMapper.Map<SpecificationsDto, Specification>(input);

           var insert=await specificationRepository.InsertAsync(specifications);
            return ObjectMapper.Map<Specification, SpecificationsDto>(insert);
        }

   //     [Authorize(Abp_BeTechPermissions.Todo.Delete)]
        public async Task<SpecificationsDto> DeleteAsync(SpecificationsDto input)
        {
            
            var specification= await specificationRepository.GetAsync( input.Id);
            if(specification == null)
            {
              return  null;
            }
       await  specificationRepository.DeleteAsync(specification,autoSave:true);
            var result=ObjectMapper.Map<Specification,SpecificationsDto>(specification);
            return result;

        }
      //  [Authorize(Abp_BeTechPermissions.Todo.Default)]
        public async Task<string> GetByNameAsync(string name)
        {
            var specification= await specificationRepository.FindAsync(x=>x.Name==name);
            if(specification == null)
            {
                throw new SepcificationNotFoundException(name);
            }
            return specification.Name;
        }
     //   [Authorize(Abp_BeTechPermissions.Todo.Default)]
        public async Task<PagedResultDto<SpecificationsDto>> GetListAsync(SpecificationsListDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Sorting))
            {
                input.Sorting = nameof(Specification.Id);
            }


            var sorting = input.Sorting;


            var query = specificationRepository
                .WithDetails(x=>x.CategorySpecifications)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(x => x.Name.Contains(input.Filter));
            }


            var totalCount = await query.CountAsync();


            var specification = await query
                .OrderBy(sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();


            var specifications = ObjectMapper.Map<List<Specification>, List<SpecificationsDto>>(specification);

            return new PagedResultDto<SpecificationsDto>(totalCount, specifications);

        }

        public async Task<List<SpecificationsDto>> GetSpecificationsByCategory(int categoryId)
        {
            var specifications = await specificationRepository
                .WithDetails(s => s.CategorySpecifications)
                .Where(s => s.CategorySpecifications.Any(cs => cs.CategoryId == categoryId)) 
                .ToListAsync();

            return ObjectMapper.Map<List<Specification>, List<SpecificationsDto>>(specifications);
        }
    //    [Authorize(Abp_BeTechPermissions.Todo.Edit)]
        public async Task<ResultView< SpecificationsDto>> UpdateAsync(SpecificationsDto input)
        {
           var specification=await specificationRepository.FirstOrDefaultAsync(x=>x.Id==input.Id);
            var oldspe=ObjectMapper.Map<Specification, SpecificationsDto>(specification);
            if (oldspe == null)
            {
                return new ResultView<SpecificationsDto>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Specification Not Found"
                };
            }
            var Mapping=ObjectMapper.Map<SpecificationsDto,Specification>(input,specification);
            var result =await specificationRepository.UpdateAsync(Mapping);
            var newspec= ObjectMapper.Map<Specification, SpecificationsDto>(result);
            return new ResultView<SpecificationsDto>()
            {
                Entity = newspec,
                IsSuccess = true,
                Message = "Successfully"
            };
        }
    }
}
