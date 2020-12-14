using PWA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWA.Network
{
    public interface INetworkHelper
    {
        Task AcceptRequest(int id);
        Task DeclineRequest(int id);
        Task DeletePermission(int id);
        Task EditPermission(Permission p);
        Task<PublicAccountModel> GetByPhoneNumber(string phoneNumber);
        Task<List<NotificationData>> GetNotificationSettings(string userID);
        Task<List<PermissionRequestModel>> GetPermissionRequests();
        Task<List<Permission>> GetPermissions();
        Task<SubjectList> GetSubjectsData();
        Task<LoginUser> Login(LoginCredential credential);
        Task SendRequest(RequestPermissionAPIModel request);
        Task<bool> TestNSLink(string link);
        Task<bool> UpdateNSLink(string link);
    }
}