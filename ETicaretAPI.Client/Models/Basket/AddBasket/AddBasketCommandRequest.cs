namespace ETicaretAPI.Client.Models.Basket.AddBasket
{
    public class AddBasketCommandRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
