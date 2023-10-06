using ETicaretAPI.Client.Models;
using ETicaretAPI.Client.Models.Category.ProductTotalOfCategories;
using ETicaretAPI.Client.Services.Category;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETicaretAPI.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService categoryService;
        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ProductTotalOfCategoriesResponse? response = await categoryService.ProductTotalofCategory();
            return View(response.CategoryProductDtos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}