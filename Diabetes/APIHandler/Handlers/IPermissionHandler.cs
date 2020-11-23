﻿using APIDataAccess.Models.Permission;
using System.Collections.Generic;

namespace APIHandler.Handlers
{
    public interface IPermissionHandler
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