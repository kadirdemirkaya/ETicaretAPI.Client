using ETicaretAPI.Client.Models.Basket.AddBasket;
using ETicaretAPI.Client.Models.Basket.ConfirmBasket;
using ETicaretAPI.Client.Models.Basket.DeleteBasket;
using ETicaretAPI.Client.Models.Basket.GetBasketForUser;
using ETicaretAPI.Client.Services.Generic;
using ETicaretAPI.Client.Statics.Urls;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ETicaretAPI.Client.Services.Basket
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IGenericService<AddBasketCommandRequest, AddBasketCommandResponse> addbasketGenericService;
        private readonly IGenericService<GetBasketForUserCommandRequest, GetBasketForUserCommandResponse> getbasketGenericService;
        private readonly IGenericService<DeleteInBasketProductCommandRequest, DeleteInBasketProductCommandResponse> deleteBasketProduct;

        public BasketService(IGenericService<AddBasketCommandRequest, AddBasketCommandResponse> addbasketGenericService, IGenericService<GetBasketForUserCommandRequest, GetBasketForUserCommandResponse> getbasketGenericService, IGenericService<DeleteInBasketProductCommandRequest, DeleteInBasketProductCommandResponse> deleteBasketProduct, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.addbasketGenericService = addbasketGenericService;
            this.getbasketGenericService = getbasketGenericService;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpContextAccessor.HttpContext.Session.GetString("JWToken"));
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<AddBasketCommandResponse> AddBasketAsync(AddBasketCommandRequest addBasketCommandRequest, AddBasketCommandResponse addBasketCommandResponse)
        {
            var response = await addbasketGenericService.AddAsync(addBasketCommandRequest, addBasketCommandResponse, ApiUrls.Basket.AddBasket);
            return response;
        }
        public async Task<GetBasketForUserCommandResponse> GetBasketForUser(GetBasketForUserCommandResponse response)
        {
            var result = await getbasketGenericService.GetAsync(response, null, ApiUrls.Basket.GetBasketForUser);
            return result;
        }

        public async Task<DeleteInBasketProductCommandResponse> DeleteBasketByGuid(DeleteInBasketProductCommandRequest request)
        {
            DeleteInBasketProductCommandResponse response = new();
            var result = await deleteBasketProduct.DeleteAsync(response, request.Id, ApiUrls.Basket.DeleteInBasketProduct);
            return result;
        }

        public async Task<ConfirmBasketCommandResponse> ConfirmBasket(ConfirmBasketCommandRequest request,string requestUrl)
        {
            string jsonContent = JsonConvert.SerializeObject(request);
            HttpResponseMessage response = await httpClient.PostAsync(requestUrl, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
            string responseContent = await response.Content.ReadAsStringAsync();
            ConfirmBasketCommandResponse commandResponse = JsonConvert.DeserializeObject<ConfirmBasketCommandResponse>(responseContent);
            return commandResponse;
        }
    }
}
