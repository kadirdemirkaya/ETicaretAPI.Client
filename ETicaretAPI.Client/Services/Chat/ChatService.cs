using ETicaretAPI.Client.Models.Chat;
using ETicaretAPI.Client.Services.Generic;

namespace ETicaretAPI.Client.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IGenericService<MessageCreateCommandRequest, MessageCreateCommandResponse> createMessage;

        public ChatService(IGenericService<MessageCreateCommandRequest, MessageCreateCommandResponse> createMessage)
        {
            this.createMessage = createMessage;
        }

        public async Task<MessageCreateCommandResponse> MessageCreateAsync(MessageDto messageDto)
        {
            MessageCreateCommandRequest request = new() { MessageDto = messageDto };
            MessageCreateCommandResponse response = new();
            var response2 = await createMessage.AddAsync(request, response, Statics.Urls.ApiUrls.Chat.MessageCreateAsync);
            return response2;
        }
    }
}
