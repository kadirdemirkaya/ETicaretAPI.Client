using ETicaretAPI.Client.Models.Communication.CommunicationCreate;
using ETicaretAPI.Client.Models.Communication.CommunicationEnd;
using ETicaretAPI.Client.Models.Communication.CommunicationEndForAppuserId;
using ETicaretAPI.Client.Models.Communication.CommunicationInfoForCommunicationPerson;
using ETicaretAPI.Client.Models.Communication.CommunicationInfoForUser;
using ETicaretAPI.Client.Models.Communication.GetRoleUser;
using ETicaretAPI.Client.Services.Generic;

namespace ETicaretAPI.Client.Services.Communication
{
    public class CommunicationService : ICommunicationService
    {
        private readonly IGenericService<GetRoleUserQueryRequest, GetRoleUserQueryResponse> getRoleUserService;
        private readonly IGenericService<CommunicationInfoForUserQueryRequest, CommunicationInfoForUserQueryResponse> communicationInfoForUser;
        private readonly IGenericService<CommunicationInfoForCommunicationPersonQueryRequest, CommunicationInfoForCommunicationPersonQueryResponse> communicationInfoForPerson;
        private readonly IGenericService<CommunicationEndCommandRequest, CommunicationEndCommandResponse> removeCommunication;
        private readonly IGenericService<CommunicationCreateCommandRequest, CommunicationCreateCommandResponse> createCommunication;
        private readonly IGenericService<CommunicationEndForAppuserIdCommandRequest, CommunicationEndForAppuserIdCommandResponse> communicationEndForId;

        public CommunicationService(IGenericService<GetRoleUserQueryRequest, GetRoleUserQueryResponse> getRoleUserService, IGenericService<CommunicationInfoForUserQueryRequest, CommunicationInfoForUserQueryResponse> communicationInfoForUser, IGenericService<CommunicationInfoForCommunicationPersonQueryRequest, CommunicationInfoForCommunicationPersonQueryResponse> communicationInfoForPerson, IGenericService<CommunicationEndCommandRequest, CommunicationEndCommandResponse> removeCommunication, IGenericService<CommunicationCreateCommandRequest, CommunicationCreateCommandResponse> createCommunication, IGenericService<CommunicationEndForAppuserIdCommandRequest, CommunicationEndForAppuserIdCommandResponse> communicationEndForId)
        {
            this.getRoleUserService = getRoleUserService;
            this.communicationInfoForUser = communicationInfoForUser;
            this.communicationInfoForPerson = communicationInfoForPerson;
            this.removeCommunication = removeCommunication;
            this.createCommunication = createCommunication;
            this.communicationEndForId = communicationEndForId;
        }

        public async Task<CommunicationCreateCommandResponse> CommunicationCreateAsync()
        {
            CommunicationCreateCommandRequest request = new();
            CommunicationCreateCommandResponse response = new();
            var response2 = await createCommunication.AddAsync(request, response, Statics.Urls.ApiUrls.Communication.CommunicationCreate);
            return response2;
        }

        public async Task<CommunicationEndCommandResponse> CommunicationEndAsync()
        {
            CommunicationEndCommandResponse response = await removeCommunication.GetAsync(Statics.Urls.ApiUrls.Communication.CommunicationEnd);
            return response;
        }

        public async Task<CommunicationEndForAppuserIdCommandResponse> CommunicationEndForAppUserIdAsync(Guid appUserId)
        {
            CommunicationEndForAppuserIdCommandResponse res = new();
            res = await communicationEndForId.GetAsync(res, appUserId, Statics.Urls.ApiUrls.Communication.CommunicationEndForAppuserId);
            return res;
        }

        public async Task<CommunicationInfoForCommunicationPersonQueryResponse> CommunicationInfoForPersonAsync()
        {
            CommunicationInfoForCommunicationPersonQueryResponse ccp = new();
            CommunicationInfoForCommunicationPersonQueryResponse response = await communicationInfoForPerson.GetAllAsync(ccp, Statics.Urls.ApiUrls.Communication.CommunicationInfoForPerson);
            return response;
        }

        public async Task<CommunicationInfoForUserQueryResponse> CommunicationInfoForUserAsync()
        {
            CommunicationInfoForUserQueryResponse response = await communicationInfoForUser.GetAsync(Statics.Urls.ApiUrls.Communication.CommunicationInfoForUser);
            return response;
        }

        public async Task<GetRoleUserQueryResponse> GetUserRoles()
        {
            GetRoleUserQueryResponse response = await getRoleUserService.GetAsync(Statics.Urls.ApiUrls.Communication.GetRoleUser);
            return response;
        }
    }
}