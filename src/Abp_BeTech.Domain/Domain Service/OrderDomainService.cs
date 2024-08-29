using Abp_BeTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Abp_BeTech.Domain_Service
{
    public class OrderDomainService : DomainService
    {
        private readonly IRepository<Order, int> _OrderRepository;

        public OrderDomainService(IRepository<Order, int> OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        //public async Task<Order> CreateOrderAsync(Order order)
        //{
        //    // جمع معرفات المنتجات المطلوبة
        //    var productIds = order.orderItems.Select(oi => oi.Product.Id).Distinct().ToList();
        //    var products = await _productRepository.GetListAsync(p => productIds.Contains(p.Id));

        //    foreach (var orderItem in order.orderItems)
        //    {
        //        var product = products.FirstOrDefault(p => p.Id == orderItem.Product.Id);

        //        if (product == null)
        //        {
        //            throw new BusinessException("ProductNotFound", $"Product with ID {orderItem.Product.Id} not found.");
        //        }

        //        if (product.Quantity < orderItem.Quantity)
        //        {
        //            throw new BusinessException("InsufficientStock", $"Insufficient stock for product with ID {orderItem.Product.Id}.");
        //        }

        //        product.Quantity -= orderItem.Quantity ?? 0;
        //        await _productRepository.UpdateAsync(product);
        //    }

        //    return order;
        //}
   //     public async Task<List<Order>> GetOrdersSortedByDateDescendingAsync()
   //     {
   //         var orders = await _OrderRepository
   //.OrderByDescending(o => o.CreationTime)
   //.ToListAsync();
   //     }
    }




}
