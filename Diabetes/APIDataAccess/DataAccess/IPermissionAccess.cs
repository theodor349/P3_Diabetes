using APIDataAccess.Models.Permission;
using System.Collections.Generic;

namespace APIDataAccess.DataAccess
{
    public interface IPermissionAccess
    {
        void Create(RequestPermissionDBModel request);
        void Delete(int id);
        void DeleteByUserId(string userId);
        UpdatePermissionDBModel Get(int id);
        List<UpdatePermissionDBModel> GetByTargetId(string targetId);
        List<UpdatePermissionDBModel> GetByWatcherId(string watcherId);
        List<UpdatePermissionDBModel> GetPendingPermissions();
        int GetPermissionActtributes(List<UpdatePermissionDBModel> permissions);
        void Update(UpdatePermissionDBModel updatedPermission);
    }
}