using APIDataAccess.Models.Permission;
using System.Collections.Generic;

namespace APIDataAccess.DataAccess
{
    public interface IPermissionAccess
    {
        void Create(RequestPermissionDBModel request);
        void Delete(int id);
        void DeleteByUserId(string userId);
        PermissionDBModel Get(int id);
        List<PermissionDBModel> GetByUserId(string userId);
        List<PermissionDBModel> GetPendingPermissions();
        int GetPermissionActtributes(List<PermissionDBModel> permissions);
        void Update(UpdatePermissionDBModel updatedPermission);
    }
}