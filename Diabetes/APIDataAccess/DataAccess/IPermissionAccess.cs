using APIDataAccess.Models.Permission;
using System.Collections.Generic;

namespace APIDataAccess.DataAccess
{
    public interface IPermissionAccess
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