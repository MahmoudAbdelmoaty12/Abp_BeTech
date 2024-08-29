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

namespace Abp_BeTech.Orders
{
    public interface IOrderAppService:IApplicationService
    {
        Task<ResultView<OrderDtos>> CreateOrderAsync(OrderDtos orderDto);
        Task<ResultDataList<GetAllOrderDto>> GetAllOrders();
        Task<ResultDataList<GetAllOrderItemDto>> GetOrderItems(int orderId);
        Task<ResultView<GetAllOrderDto>> GetOrderByIdAsync(int orderId);
        Task<ResultView<OrderDtos>> HardDeleteOrderAsync(int orderId);
        Task<ResultView<OrderDtos>> SoftDeleteOrderAsync(int orderId);
        Task<ResultView<OrderItemDto>> SoftDeleteOrderItemAsync(int orderItemId);
        Task<ResultView<OrderItemDto>> UpdateOrderItemQuantityAsync(int orderId, int orderItemId, int newQuantity);
        Task<ResultView<OrderDtos>> updateStatus(int OrderId, OrderStatus NewOrderStatus);
        Task<ResultDataList<GetAllOrderDto>> GetOrdersSortedByDateAscendingAsync();
        Task<ResultDataList<GetAllOrderDto>> GetOrdersSortedByDateDescendingAsync();
        Task<ResultDataList<GetAllOrderDto>> GetOrdersByUserIdAsync(Guid userId);
        Task<ResultDataList<GetAllOrderDto>> SearchOrdersAsync(int searchTerm);
    }
}
