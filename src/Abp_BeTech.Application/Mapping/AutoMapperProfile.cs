using Abp_BeTech.CategoryDato;
using Abp_BeTech.Categorys;
using Abp_BeTech.CategorySpecifications;
using Abp_BeTech.Models;
using Abp_BeTech.Orders;
using Abp_BeTech.Ptoduct;
using Abp_BeTech.PtoductDto;
using Abp_BeTech.ReviewDto;
using Abp_BeTech.Reviews;
using Abp_BeTech.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp_BeTech.Mapping
{
    public class AutoMapperProfile:Profile
    {
      public AutoMapperProfile() {

            CreateMap<CreateUpdateCategoryDto, Category>();
            CreateMap<Category, CategoryDtos>();
            CreateMap<CategoryDtos, Category>();
            CreateMap<CreateUpdateReviewDto, Review>();
            CreateMap<Review,ReviewDtos >();
            CreateMap<Specification,SpecificationsDto>();
            CreateMap<SpecificationsDto, Specification>();
            CreateMap<CreateUpdateCategorySpecificationsDtos, CategorySpecification>();
            CreateMap<CategorySpecification, CategorySpecificationDto>();
            CreateMap<Order, OrderDtos>().ReverseMap();
            CreateMap<OrderItem,OrderItemDto>().ReverseMap();
            CreateMap<Order,GetAllOrderDto>().ReverseMap();
            CreateMap<OrderItem, GetAllOrderItemDto>().ReverseMap();
            CreateMap<Product, GetAllProductsDtos>().ReverseMap();
            CreateMap<Product, CreateUpdateProductDtos>().ForMember(dest => dest.Images, opt => opt.Ignore()).ReverseMap() ;
            CreateMap<ProductCategorySpecificatoinDto, ProductCategorySpecifications>().ReverseMap();
            //CreateMap< Product,ProductWithSpecificationsDto>()
            //.ForMember(dest => dest.Images, opt => opt.Ignore())
            //.ForMember(dest => dest.ProductCategorySpecifications, opt => opt.MapFrom(src => src.productCategorySpecifications))
            //.ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model)).ReverseMap();
            CreateMap<IFormFile, Image>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FileName)).ReverseMap();
            CreateMap<ProductWithSpecificationsDto, Product>().ReverseMap();


        }
    }
}
