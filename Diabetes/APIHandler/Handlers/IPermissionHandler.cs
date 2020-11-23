using APIDataAccess.Models.Permission;
using System.Collections.Generic;

namespace APIHandler.Handlers
{
    public interface IPermissionHandler
    {
        void Create(RequestPermissionDBModel request);
        void Delete(int id);
        void DeleteByUserId(string userId);
        PermissionDBModel Get(int id);
        List<PermissionDBModel> GetByTargetId(string targetId);
        List<PermissionDBModel> GetByWatcherId(string watcherId);
        List<PermissionDBModel> GetPendingPermissions();
        int GetPermissionActtributes(List<PermissionDBModel> permissions);
        void Update(UpdatePermissionDBModel updatedPermission);
    }
}