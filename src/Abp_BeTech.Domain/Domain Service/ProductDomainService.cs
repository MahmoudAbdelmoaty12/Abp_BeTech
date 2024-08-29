using Abp_BeTech.Models;
using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Abp_BeTech.Domain_Service
{

    public class ProductDomainService : DomainService
    {
        private readonly IRepository<Product, int> _productRepository;
        private readonly IRepository<ProductCategorySpecifications, int> _productCategorySpecificationsRepository;
        private readonly IRepository<Image> _imageRepository;
        public ProductDomainService(
            IRepository<Product, int> productRepository,
            IRepository<ProductCategorySpecifications, int> productCategorySpecificationsRepository, IRepository<Image> ImageRepository)
        {
            _productRepository = productRepository;
            _productCategorySpecificationsRepository = productCategorySpecificationsRepository;
            _imageRepository = ImageRepository;

        }
        public async Task<Product> CreatProduct(Product product)
        {

            foreach (var formFile in product.Images)
            {
                var images = new Image
                {
                    Name = formFile.Name,
                    // = formFile.ContentType,
                    // Load file content or handle the image as needed
                };
                product.Images.Add(images);
            }

            var insertedProduct = await _productRepository.InsertAsync(product, autoSave: true);

            foreach (var specDto in product.productCategorySpecifications)
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
            var image = await _imageRepository.GetListAsync(x => x.Product.Id == product.Id);
            insertedProduct.Images = image;
            return insertedProduct;
        }
        public async Task<List<Product>> SortProductsByDescending(int categoryId, int itemsPerPage, int pageNumber)
        {
            var products = await _productRepository
         .GetListAsync(x => x.Categoryid == categoryId && !x.IsDeleted && x.Quantity > 0);



            var sortedProducts = products
                .OrderByDescending(x => x.Id)
                .Skip(itemsPerPage * (pageNumber - 1))
                .Take(itemsPerPage)
                .ToList();

            return sortedProducts;

        }



        public async Task<List<Product>> SortProductsByAscending(int categoryId, int itemsPerPage, int pageNumber)
        {
            var products = await _productRepository
         .GetListAsync(x => x.Categoryid == categoryId && !x.IsDeleted && x.Quantity > 0);



            var sortedProducts = products
                .OrderBy(x => x.Id)
                .Skip(itemsPerPage * (pageNumber - 1))
                .Take(itemsPerPage)
                .ToList();

            return sortedProducts;

        }




        public async Task<List<Product>> FilterProductsByCategory(int categoryId, int ItemsPerPage, int PageNumber){

           
                var products = _productRepository.WithDetails(x => x.Images).ToList();



                var sortedProducts = products.Where(x=>x.Categoryid==categoryId&&x.IsDeleted==false&&x.Quantity>0)

                    .Skip(ItemsPerPage * (PageNumber - 1))
                    .Take(ItemsPerPage)
                    .ToList();
            if (sortedProducts != null)
            {

                return sortedProducts;
            }
            return null;
            
            
        }
  





    }
 
}
