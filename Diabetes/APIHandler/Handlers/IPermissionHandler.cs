using APIDataAccess.Models.Permission;
using System.Collections.Generic;

namespace APIHandler.Handlers
{
    public interface IPermissionHandler
    {
        int AcceptPermissionRequest(int id);
        int Delete(int id);
        int DeleteByUserId(string userId);
        UpdatePermissionDBModel Get(int id);
        List<UpdatePermissionDBModel> GetByTargetId(string targetId);
        List<UpdatePermissionDBModel> GetByWatcherId(string watcherId);
        List<RequestPermissionDBModel> GetPendingPermissions(string userId);
        Dictionary<string, int> GetPermissionAttributes(UpdatePermissionDBModel[] permissions);
        bool IsPermissionActive(UpdatePermissionDBModel permission);
        int RequestPermission(RequestPermissionDBModel request);
        int Update(UpdatePermissionDBModel updatedPermission);
    }
}