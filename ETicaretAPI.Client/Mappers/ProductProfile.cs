using AutoMapper;
using ETicaretAPI.Client.Models.Product.AddProduct;
using ETicaretAPI.Client.Models.Product.GetAllProduct;
using ETicaretAPI.Client.Models.Product.ProducAddPhoto;

namespace ETicaretAPI.Client.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetAllProductQueryResponse, GellAllProductDto>().ReverseMap();
            CreateMap<AddProductDto, AddProductWithPhotoDto>().ReverseMap();
        }
    }
}
