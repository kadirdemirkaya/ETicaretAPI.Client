using ETicaretAPI.Client.Models.Chat;
using ETicaretAPI.Client.Services.Chat;
using ETicaretAPI.Client.Services.Communication;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.Client.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatService chatService;
        private readonly ICommunicationService communicationService;

        public ChatController(IChatService chatService, ICommunicationService communicationService)
        {
            this.chatService = chatService;
            this.communicationService = communicationService;
        }

        [HttpGet]
        public async Task<IActionResult> ChatArea(Guid cCustomerPersonId)
        {
            MessageDto messageDto = new() { CommunicationCustomerPersonId = cCustomerPersonId };
            var response = await communicationService.GetUserRoles();
            ViewBag.role = response.roles.FirstOrDefault();
            return View(messageDto);
        }

        [HttpPost]
        public async Task<IActionResult> ChatArea(MessageDto messageDto)
        {
            var response = await communicationService.GetUserRoles();
            string role = response.roles.FirstOrDefault();
            if (role == "User")
                messageDto.PersonMessage = false;
            else
                messageDto.PersonMessage = true;
            messageDto.IsSuccess = true;
            await chatService.MessageCreateAsync(messageDto);
            return View();
        }
    }
}
