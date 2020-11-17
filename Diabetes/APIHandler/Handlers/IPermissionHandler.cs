using APIDataAccess.Models.Permission;
using System.Collections.Generic;

namespace APIHandler.Handlers
{
    public interface IPermissionHandler
    {
        void Create(RequestPermissionModel request);
        void Delete(int id);
        void DeleteByUserId(string userId);
        PermissionModel Get(int id);
        List<PermissionModel> GetByUserId(string userId);
        List<PermissionModel> GetPendingPermissions();
        int GetPermissionActtributes(List<PermissionModel> permissions);
        void Update(UpdatePermissionModel updatedPermission);
    }
}