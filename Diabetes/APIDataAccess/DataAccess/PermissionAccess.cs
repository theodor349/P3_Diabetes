using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIDataAccess.DataAccess
{
    public class PermissionAccess : IPermissionAccess
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public PermissionAccess(ISqlDataAccess sqlDataAccess)
        {
            this._sqlDataAccess = sqlDataAccess;
        }
        public UpdatePermissionDBModel Get(int id)
        {
            return _sqlDataAccess.LoadData<UpdatePermissionDBModel, dynamic>(SpCommands.spPermission_Get.ToString(), new { id }, "DDB").FirstOrDefault();
        }

        public List<UpdatePermissionDBModel> GetByWatcherId(string watcherId)
        {
            return _sqlDataAccess.LoadData<UpdatePermissionDBModel, dynamic>(SpCommands.spPermission_GetByWatcherId.ToString(), new { watcherId }, "DDB");
        }

        public List<UpdatePermissionDBModel> GetByTargetId(string targetId)
        {
            return _sqlDataAccess.LoadData<UpdatePermissionDBModel, dynamic>(SpCommands.spPermission_GetByTargetId.ToString(), new { Id = targetId }, "DDB");
        }

        public int Update(UpdatePermissionDBModel updatedPermission)
        {
            return _sqlDataAccess.SaveData(SpCommands.spPermission_UpdatePermissionModel.ToString(), updatedPermission, "DDB");
        }

        public int Delete(int id)
        {
            return _sqlDataAccess.DeleteData(SpCommands.spPermission_Delete.ToString(), new {Id = id }, "DDB");
        }

        public int Create(RequestPermissionDBModel request)
        {
            return _sqlDataAccess.SaveData(SpCommands.spPermission_Create.ToString(), request, "DDB");
        }

        public int DeleteByUserId(string userId)
        {
            return _sqlDataAccess.DeleteData(SpCommands.spPermission_DeleteByUserId.ToString(), new { Id= userId }, "DDB");
        }

        public List<RequestPermissionDBModel> GetPendingPermissions(string userId)
        {
            return _sqlDataAccess.LoadData<RequestPermissionDBModel, dynamic>(SpCommands.spPermission_GetPendingPermissions.ToString(), new { id = userId }, "DDB");
        }

        public int AcceptPermissionRequest(int id) {
            return _sqlDataAccess.SaveData(SpCommands.spPermission_AcceptPermissionRequest.ToString(), new { Id = id, Accepted = true }, "DDB");
        }

        public int DenyPermissionRequest(int id) {
            return Delete(id);
        }
    }
}
