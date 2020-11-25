using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void Update(UpdatePermissionDBModel updatedPermission)
        {
            _sqlDataAccess.SaveData(SpCommands.spPermission_UpdatePermissionModel.ToString(), updatedPermission, "DDB");
        }

        public void Delete(int id)
        {
            _sqlDataAccess.DeleteData(SpCommands.spPermission_Delete.ToString(), new {Id = id }, "DDB");
        }

        public void Create(RequestPermissionDBModel request)
        {
            _sqlDataAccess.SaveData(SpCommands.spPermission_Create.ToString(), request, "DDB");
        }

        public void DeleteByUserId(string userId)
        {
            _sqlDataAccess.DeleteData(SpCommands.spPermission_DeleteByUserId.ToString(), new { Id= userId }, "DDB");
        }

        public List<RequestPermissionDBModel> GetPendingPermissions(string userId)
        {
            return _sqlDataAccess.LoadData<RequestPermissionDBModel, dynamic>(SpCommands.spPermission_GetPendingPermissions.ToString(), new { id = userId }, "DDB");
        }

        public Dictionary<string, int> GetPermissionAttributes(UpdatePermissionDBModel[] permissions)
        {
            Dictionary<string, int> flagDictionary = new Dictionary<string, int>();

            foreach(UpdatePermissionDBModel permission in permissions) {
                int flag = 0;

                // check which attributes there is access to right now
                if (IsPermissionActive(permission))
                {
                    //check if key already exists
                    if (flagDictionary.ContainsKey(permission.TargetID)) {
                        flagDictionary[permission.TargetID] = flagDictionary[permission.TargetID] | permission.Attributes;
                    } else {
                        // add flag of attributes to dictionary with permission targetID as key
                        flagDictionary.Add(permission.TargetID, permission.Attributes);
                    }
                }
            }

            return flagDictionary;
        }

        public bool IsPermissionActive(UpdatePermissionDBModel permission)
        {
            TimeSpan startTime = new TimeSpan(permission.startTime);
            TimeSpan endTime = new TimeSpan(permission.endTime);
            TimeSpan now = DateTime.Now.TimeOfDay;

            return (now >= startTime) && (now < endTime);
        }
    }
}
