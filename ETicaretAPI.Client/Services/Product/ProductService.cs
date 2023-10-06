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
using ETicaretAPI.Client.Services.Generic;
using ETicaretAPI.Client.Statics.Urls;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ETicaretAPI.Client.Services.Product
{
    public class ProductService : IProductService
    {
        #region Properties
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IGenericService<CreateProductCommandRequest, CreateProductCommandResponse> genericCreateService;
        private readonly IGenericService<GetAllProductQueryRequest, GetAllProductQueryResponse> getAllProductDtoService;
        private readonly IGenericService<ProductDeleteCommandRequest, ProductDeleteCommandResponse> deleteProductService;
        private readonly IGenericService<GetProductByGuidQueryRequest, GetProductByGuidQueryResponse> getByGuidProductService;
        private readonly IGenericService<ProductUpdateCommandRequest, ProductUpdateCommandResponse> updateProductService;
        #endregion

        #region Constructor
        public ProductService(IGenericService<CreateProductCommandRequest, CreateProductCommandResponse> genericCreateService, IGenericService<GetAllProductQueryRequest, GetAllProductQueryResponse> getAllProductDtoService, IGenericService<ProductDeleteCommandRequest, ProductDeleteCommandResponse> deleteProductService, IGenericService<GetProductByGuidQueryRequest, GetProductByGuidQueryResponse> getByGuidProductService, IGenericService<ProductUpdateCommandRequest, ProductUpdateCommandResponse> updateProductService, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            this.genericCreateService = genericCreateService;
            this.getAllProductDtoService = getAllProductDtoService;
            this.deleteProductService = deleteProductService;
            this.getByGuidProductService = getByGuidProductService;
            this.updateProductService = updateProductService;
            this.httpContextAccessor = httpContextAccessor;
            this.httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpContextAccessor.HttpContext.Session.GetString("JWToken"));
        }
        #endregion

        public async Task<CreateProductCommandResponse> ProductAddAsync(CreateProductCommandRequest productCommandRequest, CreateProductCommandResponse productCommandResponse)
        {
            var response = await genericCreateService.AddAsync(productCommandRequest, productCommandResponse, ApiUrls.ProductUrls.AddProduct);
            return response;
        }

        public async Task<GetAllProductQueryResponse> GetAllProductAsync(GetAllProductQueryResponse allProductQueryResponse)
        {
            var response = await getAllProductDtoService.GetAllAsync(allProductQueryResponse, ApiUrls.ProductUrls.GetAllProduct);
            return response;
        }

        public async Task<ProductDeleteCommandResponse> DeleteProductAsync(ProductDeleteCommandResponse deleteCommandResponse, Guid Id)
        {
            var response = await deleteProductService.DeleteAsync(deleteCommandResponse, Id, ApiUrls.ProductUrls.productDelete);
            return response;
        }

        public async Task<GetProductByGuidQueryResponse> GetByGuidProductAsync(GetProductByGuidQueryResponse getProductByGuidQuery, Guid id)
        {
            var response = await getByGuidProductService.GetAsync(getProductByGuidQuery, id, ApiUrls.ProductUrls.getProductByGuid);
            return response;
        }

        public async Task<ProductUpdateCommandResponse> UpdateProductAsync(ProductUpdateCommandRequest productUpdateRequest)
        {
            ProductUpdateCommandResponse productUpdate = new();
            var response = await updateProductService.UpdateAsync(productUpdateRequest, productUpdate, ApiUrls.ProductUrls.productUpdate);
            return response;
        }

        public async Task<GetProductWithCodeQueryResponse> GetProductWithCodeAsync(GetProductWithCodeQueryRequest request)
        {
            httpClient.DefaultRequestHeaders.Add("ProductCode", request.ProductCode.ToString());
            var response = await httpClient.GetAsync(ApiUrls.ProductUrls.getProductWithCode);
            response.EnsureSuccessStatusCode();

            httpClient.DefaultRequestHeaders.Remove("Id");

            var result = await response.Content.ReadAsStringAsync();
            var productDto = JsonConvert.DeserializeObject<GetProductWithCodeQueryResponse>(result);
            return productDto;
        }

        public async Task<ProducAddPhotoResponseCommandResponse> ProducAddPhotoAsync(ProducAddPhotoResponseCommandRequest productaddphoto)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(productaddphoto.ProductId.ToString()), "ProductId");
            foreach (var file in productaddphoto.files)
            {
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "files",
                    FileName = file.FileName
                };
                formData.Add(fileContent);
            }

            var responseRequest = await httpClient.PostAsync(ApiUrls.ProductUrls.producAddPhoto, formData);

            if (responseRequest.IsSuccessStatusCode)
            {
                string responseBody = await responseRequest.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<ProducAddPhotoResponseCommandResponse>(responseBody);
                return new() { result = content.result };
            }
            return new() { result = false };
        }

        public async Task<DeleteProductImageByProductIdResponse> DeleteProductAndImage(DeleteProductImageByProductIdRequest deleteProductImageByProductIdRequest)
        {
            httpClient.DefaultRequestHeaders.Add("ProductId", deleteProductImageByProductIdRequest.ProductId.ToString());
            var response = await httpClient.GetAsync(ApiUrls.ProductUrls.deleteProductImageByProductId);
            response.EnsureSuccessStatusCode();

            httpClient.DefaultRequestHeaders.Remove("ProductId");

            var result = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<DeleteProductImageByProductIdResponse>(result);
            return responseContent;
        }

        public async Task<GetProductImageByProductIdResponse> GetProductImages(GetProductImageByProductIdRequest getProductImageByProductId)
        {
            httpClient.DefaultRequestHeaders.Add("ProductId", getProductImageByProductId.ProductId.ToString());
            var response = await httpClient.GetAsync(ApiUrls.ProductUrls.getProductImagesByProductId);
            response.EnsureSuccessStatusCode();

            httpClient.DefaultRequestHeaders.Remove("ProductId");

            var result = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<GetProductImageByProductIdResponse>(result);
            return responseContent;
        }

        public async Task<DeleteProductImageByGuidCommandResponse> DeleteImageToProduct(DeleteProductImageByGuidCommandRequest deleteProductImageByGuid)
        {
            httpClient.DefaultRequestHeaders.Add("Path", deleteProductImageByGuid.Path);

            var response = await httpClient.PostAsync(ApiUrls.ProductUrls.deleteProductImageByGuid, null);
            response.EnsureSuccessStatusCode();

            httpClient.DefaultRequestHeaders.Remove("Path");

            var result = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<DeleteProductImageByGuidCommandResponse>(result);
            return responseContent;
        }
    }
}
