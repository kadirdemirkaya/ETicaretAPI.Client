using ETicaretAPI.Client.Models.Category.GetAllCategoryQuery;
using ETicaretAPI.Client.Models.Product.AddProduct;
using ETicaretAPI.Client.Models.Product.DeleteProduct;
using ETicaretAPI.Client.Models.Product.DeleteProductImageByGuid;
using ETicaretAPI.Client.Models.Product.DeleteProductImageWithProductId;
using ETicaretAPI.Client.Models.Product.GetAllProduct;
using ETicaretAPI.Client.Models.Product.GetByGuidProduct;
using ETicaretAPI.Client.Models.Product.GetProductImages;
using ETicaretAPI.Client.Models.Product.GetProductWithCode;
using ETicaretAPI.Client.Models.Product.ProducAddPhoto;
using ETicaretAPI.Client.Models.Product.UpdateProduct;

namespace ETicaretAPI.Client.Services.Product
{
    public interface IProductService
    {
        Task<CreateProductCommandResponse> ProductAddAsync(CreateProductCommandRequest productCommandRequest, CreateProductCommandResponse productCommandResponse);
        Task<GetAllProductQueryResponse> GetAllProductAsync(GetAllProductQueryResponse allProductQueryResponse);
        Task<ProductDeleteCommandResponse> DeleteProductAsync(ProductDeleteCommandResponse deleteCommandResponse, Guid Id);
        Task<GetProductByGuidQueryResponse> GetByGuidProductAsync(GetProductByGuidQueryResponse getProductByGuidQuery, Guid id);
        Task<ProductUpdateCommandResponse> UpdateProductAsync(ProductUpdateCommandRequest productUpdateRequest);

        Task<GetProductWithCodeQueryResponse> GetProductWithCodeAsync(GetProductWithCodeQueryRequest getProductWithCode);

        Task<ProducAddPhotoResponseCommandResponse> ProducAddPhotoAsync(ProducAddPhotoResponseCommandRequest productaddphoto);

        Task<DeleteProductImageByProductIdResponse> DeleteProductAndImage(DeleteProductImageByProductIdRequest deleteProductImageByProductIdRequest);

        Task<GetProductImageByProductIdResponse> GetProductImages(GetProductImageByProductIdRequest getProductImageByProductId);

        Task<DeleteProductImageByGuidCommandResponse> DeleteImageToProduct(DeleteProductImageByGuidCommandRequest deleteProductImageByGuid);
    }
}
