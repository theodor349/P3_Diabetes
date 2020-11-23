using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler.Handlers
{
    public class PermissionHandler : IPermissionHandler
    {
        public PermissionDBModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<PermissionDBModel> GetByUserId(string userId)
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

        public List<PermissionDBModel> GetPendingPermissions()
        {
            throw new NotImplementedException();
        }

        public void DeleteByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public int GetPermissionActtributes(List<PermissionDBModel> permissions)
        {
            throw new NotImplementedException();
        }
    }
}
