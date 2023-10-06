using ETicaretAPI.Client.Models.Communication.CommunicationCreate;
using ETicaretAPI.Client.Models.Communication.CommunicationEnd;
using ETicaretAPI.Client.Models.Communication.CommunicationEndForAppuserId;
using ETicaretAPI.Client.Models.Communication.CommunicationInfoForCommunicationPerson;
using ETicaretAPI.Client.Models.Communication.CommunicationInfoForUser;
using ETicaretAPI.Client.Models.Communication.GetRoleUser;
using ETicaretAPI.Client.Services.Communication;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.Client.Controllers
{
    public class CommunicationController : Controller
    {
        private readonly ICommunicationService communicationService;

        public CommunicationController(ICommunicationService communicationService)
        {
            this.communicationService = communicationService;
        }

        public async Task<IActionResult> Index()
        {
            GetRoleUserQueryResponse response = await communicationService.GetUserRoles();
            foreach (var role in response.roles)
            {
                if (role.ToString() == Statics.Urls.ApiUrls.Roles.User)
                    return RedirectToAction("UserCommunication");
                else if (role.ToString() == Statics.Urls.ApiUrls.Roles.CommunicationPerson)
                    return RedirectToAction("CommunicationPerson");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserCommunication()
        {
            CommunicationInfoForUserQueryResponse? response = await communicationService.CommunicationInfoForUserAsync();
            ViewBag.CommunicationCustomerPersonId = response.CommunicationForUserDto.Id;
            return View(response.CommunicationForUserDto);
        }

        [HttpGet]
        public async Task<IActionResult> CommunicationPerson()
        {
            CommunicationInfoForCommunicationPersonQueryResponse? response = await communicationService.CommunicationInfoForPersonAsync();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> CommunicationEnd()
        {
            CommunicationEndCommandResponse response = await communicationService.CommunicationEndAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CommunicationEndForAppuserId(Guid appUserId)
        {
            CommunicationEndForAppuserIdCommandResponse response = await communicationService.CommunicationEndForAppUserIdAsync(appUserId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CommunicationCreate()
        {
            CommunicationCreateCommandResponse response = await communicationService.CommunicationCreateAsync();
            if (response.isSuccess)
                return RedirectToAction("UserCommunication", "Communication");
            return View();
        }
    }
}
