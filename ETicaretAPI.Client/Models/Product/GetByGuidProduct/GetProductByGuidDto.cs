namespace ETicaretAPI.Client.Models.Product.GetByGuidProduct
{
    public class GetProductByGuidDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }

        public List<string>? Paths { get; set; }
    }
}
