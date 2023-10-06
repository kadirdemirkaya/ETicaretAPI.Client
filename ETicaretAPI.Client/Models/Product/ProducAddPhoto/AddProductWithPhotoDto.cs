namespace ETicaretAPI.Client.Models.Product.ProducAddPhoto
{
    public class AddProductWithPhotoDto
    {
        public string? Name { get; set; }
        public int? Stock { get; set; }
        public double? Price { get; set; }
        public string ProductCode { get; set; } = Guid.NewGuid().ToString();

        public Guid? CategoryId { get; set; }

        public List<IFormFile>? files { get; set; }
    }
}
