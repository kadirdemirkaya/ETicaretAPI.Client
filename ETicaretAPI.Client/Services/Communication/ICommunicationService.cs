using ETicaretAPI.Client.Models.Communication.CommunicationCreate;
using ETicaretAPI.Client.Models.Communication.CommunicationEnd;
using ETicaretAPI.Client.Models.Communication.CommunicationEndForAppuserId;
using ETicaretAPI.Client.Models.Communication.CommunicationInfoForCommunicationPerson;
using ETicaretAPI.Client.Models.Communication.CommunicationInfoForUser;
using ETicaretAPI.Client.Models.Communication.GetRoleUser;

namespace ETicaretAPI.Client.Services.Communication
{
    public interface ICommunicationService
    {
        Task<GetRoleUserQueryResponse> GetUserRoles();
        Task<CommunicationInfoForUserQueryResponse> CommunicationInfoForUserAsync();
        Task<CommunicationInfoForCommunicationPersonQueryResponse> CommunicationInfoForPersonAsync();
        Task<CommunicationEndCommandResponse> CommunicationEndAsync();

        Task<CommunicationEndForAppuserIdCommandResponse> CommunicationEndForAppUserIdAsync(Guid appUserId);

        Task<CommunicationCreateCommandResponse> CommunicationCreateAsync();
    }
}
