using AutoMapper;
using ETicaretAPI.Client.Models.Category.GetAllCategoryQuery;
using ETicaretAPI.Client.Models.Product.AddProduct;
using ETicaretAPI.Client.Models.Product.DeleteProductImageByGuid;
using ETicaretAPI.Client.Models.Product.DeleteProductImageWithProductId;
using ETicaretAPI.Client.Models.Product.GetAllProduct;
using ETicaretAPI.Client.Models.Product.GetByGuidProduct;
using ETicaretAPI.Client.Models.Product.GetProductImages;
using ETicaretAPI.Client.Models.Product.GetProductWithCode;
using ETicaretAPI.Client.Models.Product.ProducAddPhoto;
using ETicaretAPI.Client.Models.Product.UpdateProduct;
using ETicaretAPI.Client.Services.Category;
using ETicaretAPI.Client.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace ETicaretAPI.Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        public ProductController(IProductService productService, IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IMapper mapper, ICategoryService categoryService)
        {
            this.productService = productService;
            this.httpContextAccessor = httpContextAccessor;
            this.httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpContextAccessor.HttpContext.Session.GetString("JWToken"));
            this.mapper = mapper;
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            GetAllProductQueryResponse productresponse = new();
            var response = await productService.GetAllProductAsync(productresponse);
            return View(response.GellAllProductDto);
        }

        [HttpGet]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct()
        {
            GetAllCategoryQueryResponse response = await categoryService.GetAllCategoryAsync();
            ViewData["Categories"] = response.GetAllCategoryDtos.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            return View();
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductWithPhotoDto addProductWithPhotoDto)
        {
            var map = mapper.Map<AddProductDto>(addProductWithPhotoDto);
            CreateProductCommandRequest request = new()
            {
                AddProductDto = map
            };
            CreateProductCommandResponse productresponse = new();
            var response = await productService.ProductAddAsync(request, productresponse);

            if (response.message is not null)
            {
                GetProductWithCodeQueryRequest requestCode = new() { ProductCode = addProductWithPhotoDto.ProductCode.ToString() };
                var responseInfo = await productService.GetProductWithCodeAsync(requestCode);

                //RESİM EKLE
                ProducAddPhotoResponseCommandRequest requestAddProduct = new()
                {
                    files = addProductWithPhotoDto.files,
                    ProductId = responseInfo.GetProductIdWithCodeDto.Id
                };
                var responseAdd = await productService.ProducAddPhotoAsync(requestAddProduct);
                if (responseAdd.result)
                    return RedirectToAction("Index", "Product");
                else
                    return View();
            }
            return View();
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            DeleteProductImageByProductIdRequest request = new() { ProductId = Id };
            DeleteProductImageByProductIdResponse result = await productService.DeleteProductAndImage(request);

            #region gereksiz fazla oldu
            //ProductDeleteCommandResponse productresponse = new();
            //productresponse = await productService.DeleteProductAsync(productresponse, Id);
            #endregion

            if (result.result)
                return RedirectToAction("Index", "Product");
            return View();
        }

        [HttpGet]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            GetProductByGuidQueryResponse productresponse = new();
            var product = await productService.GetByGuidProductAsync(productresponse, id);

            GetProductImageByProductIdRequest request = new() { ProductId = id };
            GetProductImageByProductIdResponse response = await productService.GetProductImages(request);

            product.GetProductByGuidDto.Paths = response.Paths;

            if (product == null)
                return NotFound();
            return View(product.GetProductByGuidDto);
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            ProductUpdateCommandRequest updateCommandRequest = new() { UpdateProductDto = updateProductDto };

            var response = await productService.UpdateProductAsync(updateCommandRequest);
            if (response.result)
                return RedirectToAction("Index", "Product");
            return View();
        }


        [HttpPost]
        [Route("DeleteProductImageByGuid")]
        public async Task<IActionResult> DeleteProductImageByGuid(string path)
        {
            DeleteProductImageByGuidCommandRequest request = new() { Path = path };
            DeleteProductImageByGuidCommandResponse response = await productService.DeleteImageToProduct(request);
            if (response.result)
                return RedirectToAction("Index", "Product");
            return View();
        }
    }
}
