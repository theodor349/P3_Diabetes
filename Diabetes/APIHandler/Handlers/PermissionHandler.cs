using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler.Handlers
{
    public class PermissionHandler : IPermissionHandler
    {
        public UpdatePermissionDBModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<UpdatePermissionDBModel> GetByTargetId(string targetId)
        {
            throw new NotImplementedException();
        }
        public List<UpdatePermissionDBModel> GetByWatcherId(string watcherId)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdatePermissionDBModel updatedPermission)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(RequestPermissionDBModel request)
        {
            throw new NotImplementedException();
        }

        public List<UpdatePermissionDBModel> GetPendingPermissions()
        {
            throw new NotImplementedException();
        }

        public void DeleteByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public int GetPermissionActtributes(List<UpdatePermissionDBModel> permissions)
        {
            throw new NotImplementedException();
        }
    }
}
