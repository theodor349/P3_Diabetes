using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;

namespace APIHandler.Handlers
{
    public interface IPermissionHandler
    {
        int AcceptPermissionRequest(int id);
        void CreatePermanent(string userId);
        int Delete(int id);
        int DeleteByUserId(string userId);
        PermissionDBModel Get(int id);
        List<PermissionDBModel> GetByTargetId(string targetId);
        List<PermissionDBModel> GetByWatcherId(string watcherId);
        List<PermissionDBModel> GetPendingPermissions(string userId);
        Dictionary<string, int> GetPermissionAttributes(List<PermissionDBModel> permissions);
        int RequestPermission(RequestPermissionDBModel request);
        int Update(UpdatePermissionModel updatedPermission);
    }
}