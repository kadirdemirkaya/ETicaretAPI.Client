using ETicaretAPI.Client.Models.Order.CancelToOrderById;
using ETicaretAPI.Client.Models.Order.GetActiveOrders;
using ETicaretAPI.Client.Models.Order.GetNotActiveOrders;
using ETicaretAPI.Client.Services.Generic;
using ETicaretAPI.Client.Statics.Urls;

namespace ETicaretAPI.Client.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IGenericService<GetNotActiveOrdersQueryRequest, GetNotActiveOrdersQueryResponse> getNotActiveService;
        private readonly IGenericService<GetActiveOrdersQueryRequest, GetActiveOrdersQueryResponse> getActiveService;
        private readonly IGenericService<CancelToOrderByIdCommandRequest, CancelToOrderByIdCommandResponse> cancelToOrderService;

        public OrderService(IGenericService<GetNotActiveOrdersQueryRequest, GetNotActiveOrdersQueryResponse> getNotActiveService, IGenericService<GetActiveOrdersQueryRequest, GetActiveOrdersQueryResponse> getActiveService, IGenericService<CancelToOrderByIdCommandRequest, CancelToOrderByIdCommandResponse> cancelToOrderService)
        {
            this.getNotActiveService = getNotActiveService;
            this.getActiveService = getActiveService;
            this.cancelToOrderService = cancelToOrderService;
        }

        public async Task<CancelToOrderByIdCommandResponse> CancelOrderByIdAsync(Guid orderId)
        {
            CancelToOrderByIdCommandResponse response = new();
            var response2 = await cancelToOrderService.DeleteAsync(response, orderId, ApiUrls.Order.CancelToOrderById);
            return new() { result = response2.result };
        }

        public async Task<GetActiveOrdersQueryResponse> GetActiveOrders()
        {
            GetActiveOrdersQueryResponse response = new();
            var response2 = await getActiveService.GetAllAsync(response, ApiUrls.Order.GetActiveOrders);
            return new() { Orders = response2.Orders };
        }

        public async Task<GetNotActiveOrdersQueryResponse> GetNotActiveOrders()
        {
            GetNotActiveOrdersQueryResponse response = new();
            var response2 = await getNotActiveService.GetAllAsync(response, ApiUrls.Order.GetNotActiveOrders);
            return new() { Orders = response2.Orders };
        }
    }
}
