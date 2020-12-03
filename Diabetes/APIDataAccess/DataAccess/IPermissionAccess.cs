using APIDataAccess.Models.Permission;
using System.Collections.Generic;

namespace APIDataAccess.DataAccess
{
    public interface IPermissionAccess
    {
        int Create(RequestPermissionDBModel request);
        int Delete(int id);
        int DeleteByUserId(string userId);
        PermissionDBModel Get(int id);
        List<PermissionDBModel> GetByTargetId(string targetId);
        List<PermissionDBModel> GetByWatcherId(string watcherId);
        List<RequestPermissionDBModel> GetPendingPermissions(string userId);
        int AcceptPermissionRequest(int id);
        int DenyPermissionRequest(int id);
        int Update(UpdatePermissionModel updatedPermission);
    }
}