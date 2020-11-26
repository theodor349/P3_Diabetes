using APIDataAccess.Models.Permission;
using System.Collections.Generic;

namespace APIDataAccess.DataAccess
{
    public interface IPermissionAccess
    {
        void Create(RequestPermissionDBModel request);
        int Delete(int id);
        void DeleteByUserId(string userId);
        UpdatePermissionDBModel Get(int id);
        List<UpdatePermissionDBModel> GetByTargetId(string targetId);
        List<UpdatePermissionDBModel> GetByWatcherId(string watcherId);
        List<RequestPermissionDBModel> GetPendingPermissions(string userId);
        int AcceptPermissionRequest(int id);
        int DenyPermissionRequest(int id);
        void Update(UpdatePermissionDBModel updatedPermission);
    }
}