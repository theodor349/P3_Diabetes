using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;
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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(RequestPermissionModel request)
        {
            throw new NotImplementedException();
        }

        public void DeleteByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public List<PermissionModel> GetPendingPermissions()
        {
            throw new NotImplementedException();
        }

        public int GetPermissionActtributes(List<PermissionModel> permissions)
        {
            throw new NotImplementedException();
        }
    }
}
