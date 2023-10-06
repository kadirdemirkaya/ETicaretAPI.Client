using ETicaretAPI.Client.Models.Address;
using ETicaretAPI.Client.Models.Basket.AddBasket;
using ETicaretAPI.Client.Models.Basket.ConfirmBasket;
using ETicaretAPI.Client.Models.Basket.DeleteBasket;
using ETicaretAPI.Client.Models.Basket.GetBasketForUser;
using ETicaretAPI.Client.Models.Order;
using ETicaretAPI.Client.Services.Basket;
using ETicaretAPI.Client.Statics.Urls;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.Client.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            GetBasketForUserCommandResponse response = new();
            var result = await basketService.GetBasketForUser(response);
            return View(result.Products);
        }

        [HttpPost]
        [Route("AddBasket")]
        public async Task<IActionResult> AddBasket(AddBasket addBasket)
        {
            AddBasketCommandRequest request = new() { ProductId = addBasket.productId, Quantity = addBasket.Quantity };
            AddBasketCommandResponse response = new();
            response = await basketService.AddBasketAsync(request, response);
            if (response.result)
                return RedirectToAction("Index", "Basket");
            return View();
        }

        [HttpPost]
        [Route("DeleteBasket")]
        public async Task<IActionResult> DeleteBasket(Guid ProductId)
        {
            DeleteInBasketProductCommandRequest request = new() { Id = ProductId };
            var response = await basketService.DeleteBasketByGuid(request);
            if (response.result)
                return RedirectToAction("Index", "Basket");
            return View();
        }


        [HttpGet]
        [Route("ConfirmBasket")]
        public async Task<IActionResult> ConfirmBasket()
        {
            return View();
        }


        [HttpPost]
        [Route("ConfirmBasket")]
        public async Task<IActionResult> ConfirmBasket(AddAddressDto AddAddressDto, AddOrderDto AddOrderDto)
        {
            ConfirmBasketCommandRequest request = new() { AddAddressDto = AddAddressDto, AddOrderDto = AddOrderDto };
            ConfirmBasketCommandResponse response = await basketService.ConfirmBasket(request, ApiUrls.Basket.ConfirmBasket);
            if (response.result)
                return RedirectToAction("Index", "Order");
            return View();
        }
    }
}
