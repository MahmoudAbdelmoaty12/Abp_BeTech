using Abp_BeTech.CategoryDato;
using Abp_BeTech.Categorys;
using Abp_BeTech.Models;
using Abp_BeTech.ReviwResult;
using Abp_BeTech.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp_BeTech.Domain_Service;
using Abp_BeTech.Specifications;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Domain.ChangeTracking;
using Volo.Abp.Uow;
using Volo.Abp;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Cryptography;
using Volo.Abp.Validation.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Abp_BeTech.Orders
{
    [Authorize]
    public class OrdersApplicationService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<Order, int> _orderRepository;
        private readonly IRepository<OrderItem, int> _OrderItemRepository;
        readonly IRepository<Product, int> _productRepository;
        readonly OrderDomainService _orderDomainService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Image, int> _imageRepository;

        public OrdersApplicationService(IRepository<Order, int> orderRepository,IRepository<OrderItem,int> OrderItemRepository
            ,IRepository<Product, int> ProductRepository,OrderDomainService orderDomainService
            , IUnitOfWorkManager unitOfWorkManager,IRepository<Image,int>imageRepository

            )
        {
            this._orderRepository = orderRepository;
            this._OrderItemRepository = OrderItemRepository;
            this._productRepository = ProductRepository;
            this._orderDomainService = orderDomainService;
            _unitOfWorkManager = unitOfWorkManager;
            this._imageRepository = imageRepository;

        }
        //  [Authorize(Abp_BeTechPermissions.Todo.Create)]
     
        //   [Authorize(Abp_BeTechPermissions.Todo.Delete)]
        //public async Task<bool> DeleteAsync(int id)
        //{
        //    var oldcate = await _orderRepository.FindAsync(x => x.Id == id);

        //    if (oldcate == null)
        //    {
        //        return false;
        //    }
        //    await _orderRepository.DeleteAsync(id);

        //    return true;
        //}


        //public async Task<PagedResultDto<OrderDtos>> GetListAsync(GitOrderListDto input)
        //{
        //    if (string.IsNullOrWhiteSpace(input.Sorting))
        //    {
        //        input.Sorting = nameof(Order.Id);
        //    }


        //    var sorting = input.Sorting;


        //    var query = _orderRepository
        //        .WithDetails()
        //        .AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(input.Filter))
        //    {
        //        query = query.Where(x => x.ShippingAddress.Contains(input.Filter));
        //    }


        //    var totalCount = await query.CountAsync();


        //    var reviews = await query
        //        .OrderBy(sorting)
        //        .Skip(input.SkipCount)
        //        .Take(input.MaxResultCount)
        //        .ToListAsync();


        //    var orders = ObjectMapper.Map<List<Order>, List<OrderDtos>>(reviews);

        //    return new PagedResultDto<OrderDtos>(totalCount, orders);


        //}


        //public async Task<ResultView<OrderDtos>> GetByIdAsync(int id)
        //{
        //    var order = await _orderRepository.FindAsync(x => x.Id == id);
        //    if (order == null)
        //    {
        //        return new ResultView<OrderDtos>
        //        {
        //            Entity = null,
        //            IsSuccess = false,
        //            Message = "Category Not Found"
        //        };
        //    }

        //    var order1 = ObjectMapper.Map<Order, OrderDtos>(order);
        //    return new ResultView<OrderDtos>
        //    {
        //        Entity = order1,
        //        IsSuccess = true,
        //        Message = "Successfully"
        //    };
        //}


        // [Authorize(Abp_BeTechPermissions.Todo.Edit)]
        //public async Task<ResultView<CategoryDtos>> UpdateAsync(CreateUpdateCategoryDto input)
        //{
        //    var category = await _orderRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
        //    if (category == null)
        //    {
        //        return new ResultView<CategoryDtos>()
        //        {
        //            Entity = null,
        //            IsSuccess = false,
        //            Message = "Category Not Found"
        //        };

        //    }
        //    var mapping = ObjectMapper.Map<CreateUpdateCategoryDto, Category>(input, category);
        //    var result = await _orderRepository.UpdateAsync(mapping, autoSave: true);

        //    var cate1 = ObjectMapper.Map<Category, CategoryDtos>(result);
        //    return new ResultView<CategoryDtos>() { Entity = cate1, IsSuccess = true, Message = "Successfully" };
        //}
        //public async Task<List<ResultDataList<OrderDtos>>> GetOrdersByUserIdAsync(Guid userId)
        //    {
        //        var orders=a _orderRepository.GetAsync(x => x.IdentityUser.Id == userId)//WithDetails(x=>x.IdentityUser.Id==userId).ToList();
        //        var orderDtos = ObjectMapper.Map<List<OrderDtos>>(orders);
        //        if (user == null)
        //        {

        //            return new List<ResultDataList<OrderDtos>>()
        //            {

        //            };
        //        }
        //        var order = _orderRepository.WithDetails(x => x.orderItems).Where(x => x.IdentityUser.Id == userId).ToList();

        //    }
        public async Task<ResultView<OrderDtos>> CreateOrderAsync(OrderDtos orderDto)
        {
            var result = new ResultView<OrderDtos>();

            try
            {
                // تحويل OrderDto إلى Order بدون تعيين Product داخل OrderItems
                var order = ObjectMapper.Map<OrderDtos, Order>(orderDto);
                order.OderDate = Clock.Now; // استخدام التوقيت الحالي من ABP

                // جمع معرفات المنتجات المطلوبة
                var productIds = order.orderItems.Select(oi => oi.Product.Id).Distinct().ToList();

                // جلب المنتجات من قاعدة البيانات
                var products = await _productRepository.GetListAsync(p => productIds.Contains(p.Id));

                // التحقق وتحديث كميات المنتجات
                foreach (var orderItem in order.orderItems)
                {
                    var product = products.FirstOrDefault(p => p.Id == orderItem.Product.Id);

                    if (product == null)
                    {
                        throw new BusinessException("ProductNotFound")
                            .WithData("ProductId", orderItem.Product.Id);
                    }

                    if (product.Quantity < orderItem.Quantity)
                    {
                        throw new BusinessException("InsufficientProductQuantity")
                            .WithData("ProductId", product.Id)
                            .WithData("AvailableQuantity", product.Quantity)
                            .WithData("RequestedQuantity", orderItem.Quantity);
                    }

                    // تحديث كمية المنتج
                   product.Quantity -=(int)orderItem.Quantity;

                    // تعيين كائن المنتج إلى OrderItem
                    orderItem.UnitPrice = product.Price;
                    order.TotalPrice = (int)orderItem.Quantity *(decimal)orderItem.UnitPrice;
                    orderItem.Product = product;
               
                  //  await CurrentUnitOfWork.SaveChangesAsync();
                }

            //    order.TotalPrice = (decimal)orderDto.TotalPrice;
              
                await _orderRepository.InsertAsync(order);
                await CurrentUnitOfWork.SaveChangesAsync(); // حفظ جميع التغييرات في قاعدة البيانات
                
                // تحويل Order إلى OrderDto للإرجاع
                var createdOrderDto = ObjectMapper.Map<Order, OrderDtos>(order);

                result.IsSuccess = true;
                result.Message = "Order created successfully.";
                result.Entity = createdOrderDto;
            }
            catch (BusinessException ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                result.Entity = null; // يمكنك تخصيص عرض الأخطاء حسب الحاجة
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "An unexpected error occurred while creating the order.";
                // يمكنك تسجيل الخطأ هنا لمزيد من التحليل
            }

            return result;
        }
    



    public async Task<ResultDataList<GetAllOrderDto>> GetAllOrders()
        {
            var result = new ResultDataList<GetAllOrderDto>();

            try
            {
                var orders = await _orderRepository.GetListAsync();
                var orderDtos = ObjectMapper.Map<List<Order>,List<GetAllOrderDto>>(orders);

                result.Entities = orderDtos;
                result.Count = orderDtos.Count;
            }
            catch (Exception ex)
            {
                result.Entities = null;
                result.Count = 0;
            }

            return result;
        }
    

        public async Task<ResultView<GetAllOrderDto>> GetOrderByIdAsync(int orderId)
        {
           var order=await _orderRepository.GetAsync(x=>x.Id==orderId);

            var ord=ObjectMapper.Map<Order, GetAllOrderDto>(order);
           
            if (order == null)
            {
                return new ResultView<GetAllOrderDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Order Not Nound"
                };
            }
            else
            {
                return new ResultView<GetAllOrderDto>
                {
                    Entity = ord,
                    IsSuccess = true,
                    Message = "Successfully"
                };
            }
        }

        public async Task<ResultDataList<GetAllOrderItemDto>> GetOrderItems(int orderId)
        {

            var orderItemlist = _OrderItemRepository.WithDetails(x=>x.Product).Where(x=>x.Order.Id==orderId).ToList();
            var ordlist =new List<GetAllOrderItemDto>();
            foreach (var OrderItem in orderItemlist)
            {
                var product = await _productRepository.GetAsync(OrderItem.Id);
                var images =await _imageRepository.GetAsync(x => x.Product.Id == product.Id);
                var obj = new GetAllOrderItemDto
                {
                    OrderId = orderId,
                    ProductId = product.Id,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = OrderItem.Quantity,
                    Image = images.Name
                };
                ordlist.Add(obj);
            }

            if (orderItemlist == null)
            {
                return new ResultDataList<GetAllOrderItemDto>
                {
                    Entities = null,
                    Count = orderItemlist.Count
                };
            }
            return new ResultDataList<GetAllOrderItemDto>()
            {
                Entities = ordlist,
                Count=ordlist.Count
                
            };


        }

        public async Task<ResultDataList<GetAllOrderDto>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orderlist =await _orderRepository.GetListAsync(x=>x.CreatorId==userId);
            var orders = ObjectMapper.Map<List<Order>, List<GetAllOrderDto>>(orderlist);
            if (orderlist==null)
            {
                return new ResultDataList<GetAllOrderDto>
                {
                    Entities = null,
                    Count = orderlist.Count
                };
            }
            return new ResultDataList<GetAllOrderDto>
            {
                Entities = orders,
                Count = orders.Count
            };
        }

        public async Task<ResultDataList<GetAllOrderDto>> GetOrdersSortedByDateAscendingAsync()
        {
            try
            {
                var orders = await _orderRepository.GetListAsync();

                
                var sortedOrders = orders
                    .OrderBy(o => o.CreationTime)
                    .ToList();

                var orderDtos = ObjectMapper.Map<List<Order>, List<GetAllOrderDto>>(sortedOrders);

                return new ResultDataList<GetAllOrderDto>
                {
                    Entities = orderDtos,
                    Count = orderDtos.Count
                };
            }
            catch (Exception ex)
            {
               
                throw new BusinessException("Error while fetching and sorting orders", ex.Message);
            }
        }

        public async Task<ResultDataList<GetAllOrderDto>> GetOrdersSortedByDateDescendingAsync()
        {
            try
            {
                var orders = await _orderRepository.GetListAsync();
                orders.OrderByDescending(x => x.CreationTime);
                var ListOrders = ObjectMapper.Map<List<Order>, List<GetAllOrderDto>>(orders);
                return new ResultDataList<GetAllOrderDto>()
                {
                    Entities = ListOrders,
                    Count = ListOrders.Count
                };
            }catch(Exception ex)
            {
                throw new BusinessException("Error while fetching and sorting orders", ex.Message);
            }
        }

        public async Task<ResultView<OrderDtos>> HardDeleteOrderAsync(int orderId)
        {
            var deletedOrder = await _orderRepository.GetAsync(orderId);

            if (deletedOrder != null) {

                deletedOrder.IsDeleted = true;
                //if (deletedOrder == null)
                //    throw new Exception($"Order with ID {deletedOrder.Id} not found.");
                await _orderRepository.DeleteAsync(deletedOrder, autoSave: true);
                var orderItems = await _OrderItemRepository.GetListAsync(x => x.Order.Id == orderId);

                foreach (var item in orderItems)
                {
                    await _OrderItemRepository.DeleteAsync(item.Id, autoSave: true);
                }
              
                deletedOrder.orderItems = orderItems;
                var oldorder = ObjectMapper.Map<Order, OrderDtos>(deletedOrder);
                return new ResultView<OrderDtos>()
                {
                    Entity = oldorder,
                    IsSuccess = true,
                    Message = "Deleted successfully"
                };
            }
          
                return new ResultView<OrderDtos>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Order Not Found"
                };
            
        }

        public async Task<ResultDataList<GetAllOrderDto>> SearchOrdersAsync(int searchTerm)
        {
            var order=await _orderRepository.GetListAsync(x=>(int)x.OrderStatus==searchTerm);
            if (order != null)
            {
                var result = ObjectMapper.Map<List< Order>,List< GetAllOrderDto>>(order);
                return new ResultDataList<GetAllOrderDto>()
                {
                    Entities = result,
                    Count = result.Count,
                };
            }
            return new ResultDataList<GetAllOrderDto>()
            {
                Entities = null,
                Count = 0,
            };
        }

        public async Task<ResultView<OrderDtos>> SoftDeleteOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetAsync(orderId);
            if (order != null)
            {
                order.IsDeleted=true;
                var newOrder=await _orderRepository.UpdateAsync(order,autoSave: true);
                var ordItemlist = await _OrderItemRepository.GetListAsync(x => x.Order.Id == orderId);
                foreach (var item in ordItemlist)
                {
                    item.IsDeleted = true;
                    var neItem = await _OrderItemRepository.UpdateAsync(item);
                    }
                var ord=ObjectMapper.Map<Order, OrderDtos>(order);
                return new ResultView<OrderDtos>()
                {
                    Entity = ord,
                    IsSuccess = true,
                    Message = "Successfully"
                };
            }
            return new ResultView<OrderDtos>()
            {
                Entity = null,
                IsSuccess = false,
                Message = "Order Not Found"
            };

        }

        public async Task<ResultView<OrderItemDto>> SoftDeleteOrderItemAsync(int orderItemId)
        {
            var oldorderItem=await _OrderItemRepository.GetAsync(x=>x.Id==orderItemId);    
            if (oldorderItem != null)
            {
                oldorderItem.IsDeleted= true;
                var ordItem = await _OrderItemRepository.UpdateAsync(oldorderItem,autoSave: true);
                var result=ObjectMapper.Map<OrderItem,OrderItemDto>(ordItem);
                return new ResultView<OrderItemDto>()
                {
                    Entity = result,
                    IsSuccess = true,
                    Message = "Successfully"
                };
            }
            return new ResultView<OrderItemDto>
            {
                Entity = null,
                IsSuccess = false,
                Message = "OrderItem Not Fund"
            };
        }

        public async Task<ResultView<OrderItemDto>> UpdateOrderItemQuantityAsync(int orderId, int orderItemId, int newQuantity)
        {
            var result = new ResultView<OrderItemDto>();

            try
            {
                var orderItem = await _OrderItemRepository
            .WithDetails(oi => oi.Product) 
            .FirstOrDefaultAsync(oi => oi.Id == orderItemId);
                if (orderItem == null)
                    throw new Exception("Order item not found.");

            
                if (orderItem.Product == null)
                    throw new Exception("Product not found.");

                int quantityDifference;
                if (newQuantity > orderItem.Quantity)
                {
                    quantityDifference = newQuantity - (orderItem.Quantity ?? 0);
                    orderItem.Product.Quantity -= quantityDifference;
                }
                else
                {
                    quantityDifference = (orderItem.Quantity ?? 0) - newQuantity;
                    orderItem.Product.Quantity += quantityDifference;
                }

                orderItem.Quantity = newQuantity;
                var ordre =await _orderRepository.FirstOrDefaultAsync(x => x.Id == orderId);
                ordre.TotalPrice = (decimal)orderItem.UnitPrice * newQuantity;
                
                await _OrderItemRepository.UpdateAsync(orderItem,autoSave:true);
               await CurrentUnitOfWork.SaveChangesAsync();
               await _productRepository.UpdateAsync(orderItem.Product,autoSave:true);
                await _orderRepository.UpdateAsync(ordre,autoSave:true);
            


                var updatedOrderItemDto = ObjectMapper.Map<OrderItem,OrderItemDto>(orderItem);

                result.IsSuccess = true;
                result.Message = "Order item quantity updated successfully.";
                result.Entity = updatedOrderItemDto;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Failed to update order item quantity: {ex.Message}";
            }

            return result;
        }

        public async Task<ResultView<OrderDtos>> updateStatus(int OrderId, OrderStatus NewOrderStatus)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(x => x.Id == OrderId);
            if (order != null)
            {
                order.OrderStatus = NewOrderStatus;
                var Ored = await _orderRepository.UpdateAsync(order, autoSave: true);
                var result = ObjectMapper.Map<Order, OrderDtos>(Ored);
                return new ResultView<OrderDtos>()
                {
                    Entity = result,
                    IsSuccess = true,
                    Message = "Successfully"
                };
            }
            return new ResultView<OrderDtos>()
            {
                Entity = null,
                IsSuccess = true,
                Message = "Successfully"
            };
        }
    }
}
