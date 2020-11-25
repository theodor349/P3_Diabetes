using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;
using APIDataAccess.DataAccess;

namespace APIHandler.Handlers
{
    public class PermissionHandler : IPermissionHandler
    {
        private readonly ISqlDataAccess sqlDataAccess;
        private readonly IPermissionAccess permissionAccess;

        public PermissionHandler(ISqlDataAccess sqlDataAccess, IPermissionAccess permissionAccess) {
            this.sqlDataAccess = sqlDataAccess;
            this.permissionAccess = permissionAccess;
        }

        public UpdatePermissionDBModel Get(int id)
        {
            return permissionAccess.Get(id);
        }

        public List<UpdatePermissionDBModel> GetByTargetId(string targetId)
        {
            return permissionAccess.GetByTargetId(targetId);
        }
        public List<UpdatePermissionDBModel> GetByWatcherId(string watcherId)
        {
            return permissionAccess.GetByWatcherId(watcherId);
        }

        public List<RequestPermissionDBModel> GetPendingPermissions(string userId) {
            return permissionAccess.GetPendingPermissions(userId);
        }

        public Dictionary<string, int> GetPermissionAttributes(UpdatePermissionDBModel[] permissions) {
            Dictionary<string, int> flagDictionary = new Dictionary<string, int>();

            foreach (UpdatePermissionDBModel permission in permissions) {
                // check which attributes there is access to right now
                if (IsPermissionActive(permission)) {
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

        public bool IsPermissionActive(UpdatePermissionDBModel permission) {
            TimeSpan startTime = new TimeSpan(permission.startTime);
            TimeSpan endTime = new TimeSpan(permission.endTime);
            TimeSpan now = DateTime.Now.TimeOfDay;

            return (now >= startTime) && (now < endTime);
        }

        public void Create(RequestPermissionDBModel request) {
            permissionAccess.Create(request);
        }

        public void Update(UpdatePermissionDBModel updatedPermission)
        {
            permissionAccess.Update(updatedPermission);
        }

        public void Delete(int id)
        {
            permissionAccess.Delete(id);
        }

        public void DeleteByUserId(string userId) {
            permissionAccess.DeleteByUserId(userId);
        }
    }
}
