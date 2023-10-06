using ETicaretAPI.Client.Models.Chat;

namespace ETicaretAPI.Client.Services.Chat
{
    public interface IChatService
    {
        Task<MessageCreateCommandResponse> MessageCreateAsync(MessageDto messageDto);
    }
}
