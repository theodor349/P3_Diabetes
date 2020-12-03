﻿using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIDataAccess.DataAccess
{
    public class PermissionAccess : IPermissionAccess
    {
        private readonly ISqlDataAccess _sql;

        public PermissionAccess(ISqlDataAccess sqlDataAccess)
        {
            _sql = sqlDataAccess;
        }
        public PermissionDBModel Get(int id)
        {
            return _sql.LoadData<PermissionDBModel, dynamic>(SpCommands.spPermission_Get.ToString(), new { id }, "DDB").FirstOrDefault();
        }

        public List<PermissionDBModel> GetByWatcherId(string watcherId)
        {
            return _sql.LoadData<PermissionDBModel, dynamic>(SpCommands.spPermission_GetByWatcherId.ToString(), new { watcherId }, "DDB");
        }

        public List<PermissionDBModel> GetByTargetId(string targetId)
        {
            return _sql.LoadData<PermissionDBModel, dynamic>(SpCommands.spPermission_GetByTargetId.ToString(), new { Id = targetId }, "DDB");
        }

        public int Update(UpdatePermissionModel model)
        {
            return _sql.SaveData(SpCommands.spPermission_UpdatePermissionModel.ToString(), model, "DDB");
        }

        public int Delete(int id)
        {
            return _sql.DeleteData(SpCommands.spPermission_Delete.ToString(), new {Id = id }, "DDB");
        }

        public int Create(RequestPermissionDBModel request)
        {
            return _sql.SaveData(SpCommands.spPermission_Create.ToString(), request, "DDB");
        }

        public int DeleteByUserId(string userId)
        {
            return _sql.DeleteData(SpCommands.spPermission_DeleteByUserId.ToString(), new { Id= userId }, "DDB");
        }

        public List<RequestPermissionDBModel> GetPendingPermissions(string userId)
        {
            return _sql.LoadData<RequestPermissionDBModel, dynamic>(SpCommands.spPermission_GetPendingPermissions.ToString(), new { id = userId }, "DDB");
        }

        public int AcceptPermissionRequest(int id) {
            return _sql.SaveData(SpCommands.spPermission_AcceptPermissionRequest.ToString(), new { Id = id, Accepted = true }, "DDB");
        }

        public int DenyPermissionRequest(int id) {
            return Delete(id);
        }
    }
}
