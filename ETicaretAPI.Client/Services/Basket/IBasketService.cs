using ETicaretAPI.Client.Models.Basket.AddBasket;
using ETicaretAPI.Client.Models.Basket.ConfirmBasket;
using ETicaretAPI.Client.Models.Basket.DeleteBasket;
using ETicaretAPI.Client.Models.Basket.GetBasketForUser;

namespace ETicaretAPI.Client.Services.Basket
{
    public interface IBasketService
    {
        Task<AddBasketCommandResponse> AddBasketAsync(AddBasketCommandRequest addBasketCommandRequest, AddBasketCommandResponse addBasketCommandResponse);

        Task<GetBasketForUserCommandResponse> GetBasketForUser(GetBasketForUserCommandResponse addBasketCommandResponse);

        Task<DeleteInBasketProductCommandResponse> DeleteBasketByGuid(DeleteInBasketProductCommandRequest request);

        Task<ConfirmBasketCommandResponse> ConfirmBasket(ConfirmBasketCommandRequest request, string requestUrl);
    }
}
