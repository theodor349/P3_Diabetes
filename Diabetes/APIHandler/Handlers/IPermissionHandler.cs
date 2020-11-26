using APIDataAccess.Models.Permission;
using System.Collections.Generic;

namespace APIHandler.Handlers
{
    public interface IPermissionHandler
    {
        int AcceptPermissionRequest(int id);
        int Create(RequestPermissionDBModel request);
        int Delete(int id);
        int DeleteByUserId(string userId);
        int DenyPermissionRequest(int id);
        UpdatePermissionDBModel Get(int id);
        List<UpdatePermissionDBModel> GetByTargetId(string targetId);
        List<UpdatePermissionDBModel> GetByWatcherId(string watcherId);
        List<RequestPermissionDBModel> GetPendingPermissions(string userId);
        Dictionary<string, int> GetPermissionAttributes(UpdatePermissionDBModel[] permissions);
        int Update(UpdatePermissionDBModel updatedPermission);
    }
}