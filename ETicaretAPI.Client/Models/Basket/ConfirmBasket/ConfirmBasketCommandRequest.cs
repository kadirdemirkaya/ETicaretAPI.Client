using ETicaretAPI.Client.Models.Address;
using ETicaretAPI.Client.Models.Order;

namespace ETicaretAPI.Client.Models.Basket.ConfirmBasket
{
    public class ConfirmBasketCommandRequest
    {
        public AddAddressDto? AddAddressDto { get; set; }
        public AddOrderDto? AddOrderDto { get; set; }
    }
}
