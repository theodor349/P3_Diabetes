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
        public PermissionDBModel Get(int id)
        {
            return _sqlDataAccess.LoadData<PermissionDBModel, dynamic>("spPermission_Get", new { id }, "DDB").FirstOrDefault();
        }

        public List<PermissionDBModel> GetByWatcherId(string watcherId)
        {
            return _sqlDataAccess.LoadData<PermissionDBModel, dynamic>("spPermission_GetByWatcherId", new { watcherId }, "DDB");
        }

        public List<PermissionDBModel> GetByTargetId(string targetId)
        {
            return _sqlDataAccess.LoadData<PermissionDBModel, dynamic>("spPermission_GetByTargetId", new { targetId }, "DDB");
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

        public void DeleteByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public List<PermissionDBModel> GetPendingPermissions()
        {
            throw new NotImplementedException();
        }

        public int GetPermissionActtributes(List<PermissionDBModel> permissions)
        {
            throw new NotImplementedException();
        }
    }
}
