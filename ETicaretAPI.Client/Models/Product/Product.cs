namespace ETicaretAPI.Client.Models.Product
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? ProductCode { get; set; }
        public string? ImageProducts { get; set; }
        public Guid? Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedTime { get; set; }
        public bool IsSuccess { get; set; }
    }
}
