namespace ETicaretAPI.Client.Models.Product.GetProductWithCode
{
    public class GetProductWithCodeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string ProductCode { get; set; }
    }
}
