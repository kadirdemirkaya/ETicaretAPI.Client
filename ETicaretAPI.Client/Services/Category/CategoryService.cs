using ETicaretAPI.Client.Models.Basket.AddBasket;
using ETicaretAPI.Client.Models.Category.GetAllCategoryQuery;
using ETicaretAPI.Client.Models.Category.ProductTotalOfCategories;
using ETicaretAPI.Client.Services.Generic;
using ETicaretAPI.Client.Statics.Urls;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ETicaretAPI.Client.Services.Category
{
    public class CategoryService : ICategoryService
    {

        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IGenericService<ProductTotalOfCategoriesRequest, ProductTotalOfCategoriesResponse> productTotalOfCategory;

        public CategoryService(IGenericService<ProductTotalOfCategoriesRequest, ProductTotalOfCategoriesResponse> productTotalOfCategory, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            this.productTotalOfCategory = productTotalOfCategory;
            this.httpContextAccessor = httpContextAccessor;
            this.httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpContextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public async Task<ProductTotalOfCategoriesResponse> ProductTotalofCategory()
        {
            ProductTotalOfCategoriesResponse response = new ProductTotalOfCategoriesResponse();
            var response2 = await productTotalOfCategory.GetAllAsync(response,ApiUrls.Category.ProductTotalOfCategories);
            return new() { CategoryProductDtos = response2.CategoryProductDtos };
        }





        public async Task<GetAllCategoryQueryResponse> GetAllCategoryAsync()
        {
            httpClient.DefaultRequestHeaders.Remove("Bearer");
            var response = await httpClient.GetAsync(ApiUrls.Category.GetAllCategory);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<GetAllCategoryQueryResponse>(result);
            return responseContent;
        }
    }
}
