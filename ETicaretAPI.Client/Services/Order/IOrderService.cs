using ETicaretAPI.Client.Models.Order.CancelToOrderById;
using ETicaretAPI.Client.Models.Order.GetActiveOrders;
using ETicaretAPI.Client.Models.Order.GetNotActiveOrders;

namespace ETicaretAPI.Client.Services.Order
{
    public interface IOrderService
    {
        Task<GetNotActiveOrdersQueryResponse> GetNotActiveOrders();

        Task<GetActiveOrdersQueryResponse> GetActiveOrders();

        Task<CancelToOrderByIdCommandResponse> CancelOrderByIdAsync(Guid orderId);
    }
}
