namespace ETicaretAPI.Client.Models.Basket.AddBasket
{
    public class AddBasket
    {
        public Guid productId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
