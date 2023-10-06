using ETicaretAPI.Client.Models.Category.GetAllCategoryQuery;
using ETicaretAPI.Client.Models.Category.ProductTotalOfCategories;

namespace ETicaretAPI.Client.Services.Category
{
    public interface ICategoryService
    {
        Task<ProductTotalOfCategoriesResponse> ProductTotalofCategory();

        Task<GetAllCategoryQueryResponse> GetAllCategoryAsync();
    }
}
