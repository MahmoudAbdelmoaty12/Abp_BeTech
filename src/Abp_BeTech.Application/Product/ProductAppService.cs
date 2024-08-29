using Abp_BeTech.Domain_Service;
using Abp_BeTech.Models;
using Abp_BeTech.Ptoduct;
using Abp_BeTech.PtoductDto;
using Abp_BeTech.ReviwResult;
using Abp_BeTech.ViewResult;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Xml.Linq;
using Abp_BeTech.Specifications;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Abp_BeTech.Permissions;
using Microsoft.AspNetCore.Authorization;
namespace Abp_BeTech.Products
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductCategorySpecifications> _productCategorySpecificationsRepository;
        private readonly IRepository<Image> _imageRepository;
        private IMapper _mapper;
        private readonly ProductDomainService _productDomainService;
        public ProductAppService(IRepository<Product> productRepository, IRepository<ProductCategorySpecifications> productCategorySpecifictions,
            IRepository<Image> imageRepository,IMapper mapper,ProductDomainService productDomainService
            )
        {
            this._productRepository = productRepository;
            this._productCategorySpecificationsRepository = productCategorySpecifictions;
            this._imageRepository = imageRepository;
           this._mapper = mapper;
            this._productDomainService = productDomainService;
        }
        [Authorize(Abp_BeTechPermissions.Product.Default)]
        public async Task<ResultView<ProductCategorySpecificationsListDto>> Create(ProductWithSpecificationsDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
      
       
            
         
            foreach (var formFile in productDto.Images)
            {
                var images = new Image
                {
                    Name = formFile.Name,
                 
                };
                product.Images.Add(images);
            }

            var insertedProduct = await _productRepository.InsertAsync(product, autoSave: true);

            foreach (var specDto in productDto.productCategorySpecificatoinDto)
            {
                var productSpec = new ProductCategorySpecifications
                {
                    ProductId = insertedProduct.Id,
                    CategoryId = specDto.CategoryId,
                    SpecificationId = specDto.SpecificationId,
                    Value = specDto.Value
                };

                await _productCategorySpecificationsRepository.InsertAsync(productSpec, autoSave: true);


            }
            var prod = ObjectMapper.Map<Product, CreateUpdateProductDtos>(product);
    
     
           prod.Images = productDto.Images;
           
            var eeew = await _productCategorySpecificationsRepository.GetListAsync(x => x.ProductId == product.Id);
            var df = ObjectMapper.Map<List<ProductCategorySpecifications>, List<ProductCategorySpecificatoinDto>>(eeew);

            var eee = new ProductCategorySpecificationsListDto
            {
                ProductCategorySpecifications = df,
                CreateUpdateProductDtos = prod
            };

            return new ResultView<ProductCategorySpecificationsListDto>
            {
                Entity = eee,
                IsSuccess = true,
                Message = "Successfully"
            };
        }


        public async Task<ResultView<GetProductSpecificationNameValueDtos>> GetOne(int id)
        {
            var product = await _productRepository.GetAsync(x => x.Id == id);
         
            if (product == null)
            {
                return new ResultView<GetProductSpecificationNameValueDtos>()
                {
                    Entity = null, IsSuccess = false,Message="Product Not Found"
                } ;
            }
            var ProductCategorySpecification = _productCategorySpecificationsRepository.WithDetails(x => x.Specification).Where(x => x.ProductId == id);
          
            var prod = _mapper.Map<GetAllProductsDtos>(product);
          //  prod.DiscountValue = product.DiscuontValue;
            var list = new List<GetSpecificationsNameValueDtos>();
            foreach (var item in ProductCategorySpecification)
            {
                var GetSpecificationsNameValueDtos = new GetSpecificationsNameValueDtos();
                GetSpecificationsNameValueDtos.Name = item.Specification.Name;
                GetSpecificationsNameValueDtos.Value = item.Value;
                list.Add(GetSpecificationsNameValueDtos);
            }
            var image = await _imageRepository.GetListAsync(x => x.Product.Id == product.Id);
            foreach (var irem in image)
            {
                prod.Images.Add(irem.Name);
            }
            var GetProductSpecificationNameValueDtos = new GetProductSpecificationNameValueDtos()
            {
                productsDtos = prod,
                specificationsNameValueDtos = list
            };
            return new ResultView<GetProductSpecificationNameValueDtos>()
            {
                Entity = GetProductSpecificationNameValueDtos,
                IsSuccess = true,
                Message = "Successfolly"
            };
        }
        [Authorize(Abp_BeTechPermissions.Product.Delete)]
        public async Task<ResultView<ProductCategorySpecificationsListDto>> SoftDelete(int productId)
        {
            var OldProduct = await _productRepository.GetAsync(x => x.Id == productId);
            if (OldProduct != null)
            {
                OldProduct.IsDeleted = true;


                var ProductCatSpec =await  _productCategorySpecificationsRepository.GetListAsync(x => x.ProductId == productId);

                foreach (var productCategorySpec in ProductCatSpec)
                {
                    productCategorySpec.IsDeleted= true;
                }
           
                var DeletedProductDto = _mapper.Map<CreateUpdateProductDtos>(OldProduct);
                var ProductCatSpecDtos = _mapper.Map<List<ProductCategorySpecificatoinDto>>(ProductCatSpec);

                var ProductCatSpecListDtos = new ProductCategorySpecificationsListDto { CreateUpdateProductDtos = DeletedProductDto, ProductCategorySpecifications = ProductCatSpecDtos };

                return new ResultView<ProductCategorySpecificationsListDto>
                {
                    Entity = ProductCatSpecListDtos,
                    IsSuccess = true,
                    Message = "Product Deleted Successfully !"

                };
            }

            return new ResultView<ProductCategorySpecificationsListDto>
            {
                Entity = null,
                IsSuccess = false,
                Message = "failed to delete this product !"

            };

        }
        [Authorize(Abp_BeTechPermissions.Product.Delete)]
        public async Task<ResultView<ProductCategorySpecificationsListDto>> HardDelete(int productId)
        {
            var product = await _productRepository.GetAsync(x => x.Id == productId);
            if (product != null)
            {

                await _productRepository.DeleteAsync(product, autoSave: true);

                var ProductCatSpe = await _productCategorySpecificationsRepository.GetListAsync(x=>x.ProductId==productId);
            

                foreach (var productCatSpec in ProductCatSpe)
                {
                    await _productCategorySpecificationsRepository.DeleteAsync(productCatSpec, autoSave: true);
                 
                }

               

             
                var ProductCatSp = await _productCategorySpecificationsRepository.GetListAsync(x => x.ProductId == productId);

                var DeletedProductDto = _mapper.Map<CreateUpdateProductDtos>(product);
                var productCategorySpecificationsListtDtos = _mapper.Map<List<ProductCategorySpecificatoinDto>>(ProductCatSp);

                var productCategorySpecificationsListDto = new ProductCategorySpecificationsListDto
                {
                    CreateUpdateProductDtos = DeletedProductDto,
                    ProductCategorySpecifications = productCategorySpecificationsListtDtos
                };

                return new ResultView<ProductCategorySpecificationsListDto>
                {
                    Entity = productCategorySpecificationsListDto,
                    IsSuccess = true,
                    Message = "Product Deleted Sucessfully"
                };
            }

            return new ResultView<ProductCategorySpecificationsListDto>
            {
                Entity = null,
                IsSuccess = false,
                Message = "Product Not Found"
            };
        }

        public async Task<ResultDataList<GetAllProductsDtos>> SortProductsByDesending(int categoryId, int ItemsPerPage, int PageNumber)
        {
            //  var product=await _productDomainService.SortProductsByDescending(categoryId, ItemsPerPage, PageNumber);

            var products = _productRepository.WithDetails(x => x.Images).ToList();
            if (products != null && PageNumber > 0)
            {
                var sortedProducts = products.Where(x => x.Categoryid == categoryId && x.IsDeleted == false && x.Quantity > 0)
                        .OrderByDescending(x => x.Id)
                       .Skip(ItemsPerPage * (PageNumber - 1))
                       .Take(ItemsPerPage)
                       .ToList();
                var productDto = sortedProducts.Select(p => new GetAllProductsDtos()
                {    Id = p.Id,
                    Brand = p.Brand,
                    Warranty = p.Warranty,
                    Price = p.Price,
                    Description = p.Description,
                    DiscountValue = p.DiscountValue,
                    DiscountedPrice = p.DiscountedPrice,
                    Categoryid = p.Categoryid,
                    Quantity = p.Quantity,
                    Model = p.Model,
                    Images = p.Images.Select(x => x.Name).ToList(),

                }).ToList();

                var totalcount = _productRepository.GetListAsync().Result
                 .Where(p => p.IsDeleted == false).Count();
                return new ResultDataList<GetAllProductsDtos>()
                {
                    Entities = productDto,
                    Count = totalcount,
                };
            }

            return new ResultDataList<GetAllProductsDtos>()
            {
                Entities = null,
                Count = 0
            };

        }


        public async Task<ResultDataList<GetAllProductsDtos>> SortProductsByAscending(int categoryId, int ItemsPerPage, int PageNumber)
        {
            // var products=await _productDomainService.SortProductsByAscending(categoryId, ItemsPerPage, PageNumber);
            var products = _productRepository.WithDetails(x => x.Images).ToList();
            if (products != null && PageNumber > 0)
            {
                var sortedProducts = products.Where(x => x.Categoryid == categoryId && x.IsDeleted == false && x.Quantity > 0)
                        .OrderBy(x=>x.Id)
                       .Skip(ItemsPerPage * (PageNumber - 1))
                       .Take(ItemsPerPage)
                       .ToList();
                var productDto = sortedProducts.Select(p => new GetAllProductsDtos()
                {      Id = p.Id,
                    Brand = p.Brand,
                    Warranty = p.Warranty,
                    Price = p.Price,
                    Description = p.Description,
                    DiscountValue = p.DiscountValue,
                    DiscountedPrice = p.DiscountedPrice,
                    Categoryid = p.Categoryid,
                    Quantity = p.Quantity,
                    Model = p.Model,
                    Images = p.Images.Select(x => x.Name).ToList(),

                }).ToList();

                var totalcount = _productRepository.GetListAsync().Result
                 .Where(p => p.IsDeleted == false).Count();
                return new ResultDataList<GetAllProductsDtos>()
                {
                    Entities = productDto,
                    Count = totalcount,
                };
            }

            return new ResultDataList<GetAllProductsDtos>()
            {
                Entities = null,
                Count = 0
            };

        }

        public async Task<ResultDataList<GetAllProductsDtos>> GetAllPagination(int ItemsPerPage, int PageNumber)
        {

            var products = _productRepository.WithDetails(x => x.Images).ToList();
            if (products != null && PageNumber > 0)
            {
                var sortedProducts = products.Where(x => x.IsDeleted == false && x.Quantity > 0)

                    .Skip(ItemsPerPage * (PageNumber - 1))
                    .Take(ItemsPerPage)
                    .ToList();

                var productDto = products.Select(p => new GetAllProductsDtos()
                {     Id = p.Id,
                    Brand = p.Brand,
                    Warranty = p.Warranty,
                    Price = p.Price,
                    Description = p.Description,
                    DiscountValue = p.DiscountValue,
                    DiscountedPrice = p.DiscountedPrice,
                    Categoryid = p.Categoryid,
                    Quantity = p.Quantity,
                    Model = p.Model,
                    Images = p.Images.Select(x => x.Name).ToList(),

                }).ToList();

                var totalcount = _productRepository.GetListAsync().Result
                 .Where(p => p.IsDeleted == false).Count();
                return new ResultDataList<GetAllProductsDtos>()
                {
                    Entities = productDto,
                    Count = totalcount,
                };
            }

            return new ResultDataList<GetAllProductsDtos>()
            {
                Entities = null,
                Count = 0
            };
        }


        [Authorize(Abp_BeTechPermissions.Product.Default)]
        public async Task<ResultView<ProductWithSpecificationsDto>> Update(ProductWithSpecificationsDto productDto)
        {
         
            var product = await _productRepository.GetAsync(x => x.Id == productDto.Id);

            if (product == null)
            {
                return new ResultView<ProductWithSpecificationsDto>
                {
                    Entity = null,
                    Message = "Product not found"
                };
            }

            product.Description = productDto.Description;
            product.Brand = productDto.Brand;
            product.Model = productDto.Model;
            product.Price = productDto.Price;
            product.DiscountValue = productDto.DiscountValue;
            product.DiscountValue = productDto.DiscountedPrice;
            product.Warranty = productDto.Warranty;
            product.Quantity = (int)productDto.Quantity;
            product.Categoryid = productDto.Categoryid;

            
            var existingImages = await _imageRepository.GetListAsync(x => x.Id == productDto.Id);
            foreach (var image in existingImages)
            {
                await _imageRepository.DeleteAsync(image);
            }

            if (productDto.Images != null && productDto.Images.Count > 0)
            {
                foreach (var formFile in productDto.Images)
                {
                    var newImage = new Image
                    {
                     
                        Name = await SaveImageAsync(formFile) 
                    };
                    await _imageRepository.InsertAsync(newImage);
                }
            }

            var existingSpecs = await _productCategorySpecificationsRepository.GetListAsync(x => x.ProductId == productDto.Id);
            foreach (var spec in existingSpecs)
            {
                await _productCategorySpecificationsRepository.DeleteAsync(spec);
            }

            foreach (var specDto in productDto.productCategorySpecificatoinDto)
            {
                var newSpec = new ProductCategorySpecifications
                {    
                    CategoryId = specDto.CategoryId,
                    ProductId = product.Id,
                    SpecificationId = specDto.SpecificationId,
                    Value = specDto.Value
                };
                await _productCategorySpecificationsRepository.InsertAsync(newSpec);
            }

            await _productRepository.UpdateAsync(product);

            return new ResultView<ProductWithSpecificationsDto>
            {
                IsSuccess = true,
                Entity = productDto
            };
        }

        private async Task<string> SaveImageAsync(IFormFile formFile)
        {
           
            return "path_to_saved_image";
        }


        public async Task<ResultDataList<GetAllProductsDtos>> SearchProduct(string ModelName, int ItemsPerPage, int PageNumber)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(ModelName))
                {
                    throw new ArgumentException("Name cannot be empty or whitespace.");
                }

                if (PageNumber <= 0)
                {
                    throw new ArgumentException("Page number must be greater than zero");
                }
                var products =  _productRepository.WithDetails(x => x.Images).ToList();
           
                var sortedProducts = products.Where(x=>x.Model==ModelName&&x.IsDeleted==false&&x.Quantity>0)
                    .OrderByDescending(x => x.Id)
                    .Skip(ItemsPerPage * (PageNumber - 1))
                    .Take(ItemsPerPage).Select(product =>
                    new GetAllProductsDtos()
                    {    Id=product.Id,
                        Price = product.Price,
                        Brand = product.Brand,
                        DiscountedPrice = product.DiscountedPrice,
                        Quantity = product.Quantity,
                        Images = product.Images.Select(x => x.Name).ToList(),
                        Description = product.Description,
                        Warranty = product.Warranty,
                        Categoryid = product.Categoryid,
                        Model = product.Model,
                    })
                    .ToList();
                var totalCount = (await _productRepository.GetListAsync(x => x.Model == ModelName))
                                .Where(p => !p.IsDeleted)
                                .Count();

                //   var prod=_mapper.Map<List<GetAllProductsDtos>>(sortedProducts);

                return new ResultDataList<GetAllProductsDtos>()
                {
                    Entities = sortedProducts,
                    Count = totalCount

                };
            }
            catch (Exception ex)
            {
                var resultDataList = new ResultDataList<GetAllProductsDtos>()
                {
                    Entities = null,
                    Count = 0
                };
                return resultDataList;
            }
        }

        public async Task<ResultDataList<GetAllProductsDtos>> FilterProductsByCategory(int categoryId, int ItemsPerPage, int PageNumber)
        {
            var products = _productRepository.WithDetails(x => x.Images).ToList();
            if (products!=null&&  PageNumber > 0)
            {
                var sortedProducts = products.Where(x => x.IsDeleted == false && x.Quantity > 0)

                    .Skip(ItemsPerPage * (PageNumber - 1))
                    .Take(ItemsPerPage)
                    .ToList();

                var productDto = products.Select(p => new GetAllProductsDtos()
                {       Id=p.Id,
                      Brand = p.Brand,
                      Warranty = p.Warranty,
                      Price= p.Price,
                      Description= p.Description,
                      DiscountValue= p.DiscountValue,
                    DiscountedPrice= p.DiscountedPrice,
                    Categoryid= categoryId,
                    Quantity= p.Quantity,
                    Model=p.Model,
                    Images=p.Images.Select(x=>x.Name).ToList(),

                }).ToList();      

                var totalcount = _productRepository.GetListAsync(x=>x.Categoryid==categoryId).Result
                 .Where(p => p.IsDeleted == false).Count();
                return new ResultDataList<GetAllProductsDtos>()
                {
                    Entities = productDto,
                    Count = totalcount,
                };
            }

            return new ResultDataList<GetAllProductsDtos>()
            {
                Entities = null,
                Count = 0
            };
        }

        public async Task<ResultDataList<GetAllProductsDtos>> FilterProducts(FiltterProductDto filterProductsDto, int categoryId, int ItemsPerPage, int PageNumber)
        {
            
            var query = _productRepository.WithDetails(x => x.Images);
                                                          
            query.Where(x=>x.Categoryid== categoryId&&x.IsDeleted==false&&x.Quantity>0);
            if (query == null)
            {
                if (filterProductsDto.DiscountValue.HasValue)
                {
                    query = query.Where(x => x.DiscountValue == filterProductsDto.DiscountValue.Value);
                }

                if (!string.IsNullOrEmpty(filterProductsDto.Warranty))
                {
                    query = query.Where(x => x.Warranty == filterProductsDto.Warranty);
                }

                if (!string.IsNullOrEmpty(filterProductsDto.Brand))
                {
                    query = query.Where(x => x.Brand == filterProductsDto.Brand);
                }

                if (filterProductsDto.MinPrice.HasValue)
                {
                    query = query.Where(x => x.Price >= filterProductsDto.MinPrice.Value);
                }

                if (filterProductsDto.MaxPrice.HasValue)
                {
                    query = query.Where(x => x.Price <= filterProductsDto.MaxPrice.Value);
                }

                var totalItems = await query.CountAsync();

                var products = await query
                    .OrderBy(x => x.Id)
                    .Skip((PageNumber - 1) * ItemsPerPage)
                    .Take(ItemsPerPage)
                    .Select(x => new GetAllProductsDtos
                    {
                        Id = x.Id,
                        Brand = x.Brand,
                        Model = x.Model,
                        Price = x.Price,
                        DiscountedPrice = x.DiscountedPrice,
                        Warranty = x.Warranty,
                        Categoryid = x.Categoryid,
                        DiscountValue = x.DiscountValue,
                        Quantity = x.Quantity,
                        Images = x.Images.Select(x => x.Name).ToList(),
                    })
                    .ToListAsync();

                return new ResultDataList<GetAllProductsDtos>
                {
                    Entities = products,
                    Count = totalItems
                };
            }
            var dd = query.OrderBy(x => x.Id).ToList();
            var eeeee = _mapper.Map<List<GetAllProductsDtos>>(dd);
            return new ResultDataList<GetAllProductsDtos>
            {
                Entities = eeeee,
                Count = eeeee.Count()
            };
        }

        public async Task<List<string>> GetAllBrands()
        {
         
            var brands = await _productRepository.GetListAsync();
              var listprands = brands.OrderBy(x => x.Id).Select(x => x.Brand).ToList();
               return listprands;

        }
        public async Task<List<string>> GetBrandsProduct(int categoryid)
        {
            var brands = await _productRepository.GetListAsync(x => x.Categoryid == categoryid);
            var listbrands = brands.OrderBy(x => x.Id).Select(x => x.Brand).ToList();
            return listbrands;
            
        }

        public async Task<ResultDataList<GetAllProductsDtos>> FilterDiscountedProducts()
        {
            var products = _productRepository.WithDetails(x => x.Images).ToList();
            var listproduct = products.OrderBy(x => x.Id).Where(x=>x.DiscountValue!=null &&x.IsDeleted==false&& x.Quantity>0).Select(
                x=>new GetAllProductsDtos()
                {    Id=x.Id,
                    Brand=x.Brand,
                    DiscountValue=x.DiscountValue,
                    Warranty=x.Warranty,
                    Price=x.Price,
                    Description=x.Description,
                    Categoryid=x.Categoryid,
                    Model=x.Model,
                    DiscountedPrice=x.DiscountValue,
                    Quantity=x.Quantity,
                    Images=x.Images.Select(x=>x.Name).ToList()


                }
                
                
                ).ToList();
      

            return new ResultDataList<GetAllProductsDtos>()
            {
                Entities = listproduct,
                Count = listproduct.Count()
            };
        }

        public async Task<ResultDataList<GetAllProductsDtos>> FilterNewlyAddedProducts(int count)
        {
            try
            {
                if (count <= 0)
                {
                    throw new ArgumentException("The count must be greater than zero");
                }


                var products = await _productRepository.GetListAsync();
                var listproduct=products.OrderByDescending(x=>x.DeletionTime)
                               .Where(p =>p.CreationTime.Equals(DateTime.Now) &&  p.IsDeleted == false && p.Quantity > 0).Take(count)
                               .Select(p => new GetAllProductsDtos
                               {
                                   Id = p.Id,
                                   Model = p.Model,
                                   Description = p.Description,
                                   Brand = p.Brand,
                                   Categoryid = p.Categoryid,
                                  
                                   Price = p.Price,
                                   Quantity = p.Quantity,
                                   DiscountValue = p.DiscountValue,
                                   DiscountedPrice = p.Price - (p.Price * p.DiscountValue / 100),
                                   IsDeleted = p.IsDeleted,
                                   Images = p.Images.Select(i => i.Name).ToList()

                               }).ToList();

                var ProductsDto = _mapper.Map<List<GetAllProductsDtos>>(products);
                var resultDataLists = new ResultDataList<GetAllProductsDtos>()
                {
                    Entities = ProductsDto,
                    Count = ProductsDto.Count()
                };
                return resultDataLists;
            }
            catch (Exception ex)
            {
                var resultDataLists = new ResultDataList<GetAllProductsDtos>()
                {
                    Entities = null,
                    Count = 0
                };
                return resultDataLists;
            }


        }
    }
}

