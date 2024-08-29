using Abp_BeTech.CategoryDato;
using Abp_BeTech.Models;
using Abp_BeTech.ReviewDto;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Json.SystemTextJson.JsonConverters;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using static Abp_BeTech.Permissions.Abp_BeTechPermissions;
using Review = Abp_BeTech.Models.Review;
using NUnit.Framework;
using Microsoft.AspNetCore.Authorization;
using Abp_BeTech.Permissions;
using Abp_BeTech.Exceptionv;
using Abp_BeTech.ViewResult;
using Abp_BeTech.Categorys;

namespace Abp_BeTech.Reviews
{
    public class ReviewAppService : ApplicationService, IReviewAppService
    {
        private readonly IRepository<Review, int> reviewRepository;
        public ReviewAppService(IRepository<Review, int> reviewRepository)
        {

            this.reviewRepository = reviewRepository;

        }
           [Authorize]
        public async Task<ReviewDtos> CreateAsync(CreateUpdateReviewDto input)
        {
            var review = ObjectMapper.Map<CreateUpdateReviewDto, Review>(input);
            var insert = await reviewRepository.InsertAsync(review, autoSave: true);

            return ObjectMapper.Map<Review, ReviewDtos>(insert);

        }
           [Authorize]
        public async Task<bool> DeleteAsync(int id)
        {
            var review = await reviewRepository.GetAsync(id);
            if (review == null)
            {

                return false;
            }
            reviewRepository.DeleteAsync(review);

            return true;
        }

        public async Task<PagedResultDto<ReviewDtos>> GetListAsync(GetReviewListDtos input)
        {

            if (string.IsNullOrWhiteSpace(input.Sorting))
            {
                input.Sorting = nameof(Review.Id);
            }


            var sorting = input.Sorting;


            var query = reviewRepository
                .WithDetails(review => review.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(review => review.Comment.Contains(input.Filter));
            }


            var totalCount = await query.CountAsync();


            var reviews = await query
                .OrderBy(sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();


            var reviewDtos = ObjectMapper.Map<List<Review>, List<ReviewDtos>>(reviews);

            return new PagedResultDto<ReviewDtos>(totalCount, reviewDtos);
        }

          [Authorize]
        public async Task<ResultView<ReviewDtos>> UpdateAsync(CreateUpdateReviewDto input)
        {
            var oldreveiw = await reviewRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            var review = ObjectMapper.Map<Review, ReviewDtos>(oldreveiw);
            if (review == null)
            {
                return new ResultView<ReviewDtos>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Review Not Found"
                };

            }
            var mapping = ObjectMapper.Map<CreateUpdateReviewDto, Review>(input, oldreveiw);
            var result = await reviewRepository.UpdateAsync(mapping, autoSave: true);

            var cate1 = ObjectMapper.Map<Review, ReviewDtos>(result);
            return new ResultView<ReviewDtos>() { Entity = cate1, IsSuccess = true, Message = "Successfully" };
        }
    }
}
