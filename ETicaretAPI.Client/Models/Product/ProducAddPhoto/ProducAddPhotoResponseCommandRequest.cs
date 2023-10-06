namespace ETicaretAPI.Client.Models.Product.ProducAddPhoto
{
    public class ProducAddPhotoResponseCommandRequest
    {
        public List<IFormFile> files { get; set; }
        public Guid ProductId { get; set; }
    }
}
