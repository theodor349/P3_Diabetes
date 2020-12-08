using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;
using APIDataAccess.DataAccess;
using System.Globalization;

namespace APIHandler.Handlers
{
    public class PermissionHandler : IPermissionHandler
    {
        private readonly IPermissionAccess _pa;

        public PermissionHandler(IPermissionAccess permissionAccess)
        {
            _pa = permissionAccess;
        }

        public PermissionDBModel Get(int id)
        {
            return _pa.Get(id);
        }

        public List<PermissionDBModel> GetByTargetId(string targetId)
        {
            return _pa.GetByTargetId(targetId);
        }

        public List<PermissionDBModel> GetByWatcherId(string watcherId)
        {
            return _pa.GetByWatcherId(watcherId);
        }

        public List<RequestPermissionDBModel> GetPendingPermissions(string userId)
        {
            return _pa.GetPendingPermissions(userId);
        }

        public Dictionary<string, int> GetPermissionAttributes(PermissionDBModel[] permissions)
        {
            Dictionary<string, int> flagDictionary = new Dictionary<string, int>();

            foreach (PermissionDBModel permission in permissions)
            {
                if (permission.IsActive(DateTime.Now))
                {
                    if (flagDictionary.ContainsKey(permission.TargetID))
                    {
                        flagDictionary[permission.TargetID] = flagDictionary[permission.TargetID] | permission.Attributes;
                    }
                    else
                    {
                        flagDictionary.Add(permission.TargetID, permission.Attributes);
                    }
                }
            }

            return flagDictionary;
        }

        public int RequestPermission(RequestPermissionDBModel request)
        {
            return _pa.Create(request);
        }

        public int Update(UpdatePermissionModel updatedPermission)
        {
            return _pa.Update(updatedPermission);
        }

        public int Delete(int id)
        {
            return _pa.Delete(id);
        }

        public int DeleteByUserId(string userId)
        {
            return _pa.DeleteByUserId(userId);
        }

        public int AcceptPermissionRequest(int id)
        {
            return _pa.AcceptPermissionRequest(id);
        }

        public void CreatePermanent(string userId)
        {
            _pa.CreatePermanent(userId);
        }
    }
}
