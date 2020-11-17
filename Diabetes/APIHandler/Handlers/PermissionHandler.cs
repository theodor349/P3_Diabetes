using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler.Handlers
{
    public class PermissionHandler
    {
        public PermissionModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<PermissionModel> GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdatePermissionModel updatedPermission)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Create(RequestPermissionModel request)
        {
            throw new NotImplementedException();
        }

        public List<PermissionModel> GetPendingPermissions()
        {
            throw new NotImplementedException();
        }
        
        public void DeleteByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public int GetPermissionActtributes(int id)
        {
            throw new NotImplementedException();
        }
    }
}
