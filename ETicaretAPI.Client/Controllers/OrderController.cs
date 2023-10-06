using ETicaretAPI.Client.Models.Order.CancelToOrderById;
using ETicaretAPI.Client.Models.Order.GetActiveOrders;
using ETicaretAPI.Client.Models.Order.GetNotActiveOrders;
using ETicaretAPI.Client.Services.Order;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.Client.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNotActiveOrders()
        {
            GetNotActiveOrdersQueryResponse response = await orderService.GetNotActiveOrders();
            return View(response.Orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveOrders()
        {
            GetActiveOrdersQueryResponse response = await orderService.GetActiveOrders();
            return View(response.Orders);
        }


        [HttpPost]
        public async Task<IActionResult> CancelToOrderById(Guid orderId)
        {
            CancelToOrderByIdCommandResponse response = await orderService.CancelOrderByIdAsync(orderId);
            if (response.result)
                return RedirectToAction("Index","Order");
            return View();
        }
    }
}
