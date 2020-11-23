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
            var model = _sqlDataAccess.LoadData<PermissionDBModel, dynamic>("spPermission_Get", new { id }, "DDB").FirstOrDefault();
            

            if (model == null)
                return null;
            else
                return new PermissionDBModel()
                {
                    Id = model.Id,
                    WatcherID = model.WatcherID,
                    TargetID = model.TargetID,
                    StartDate = model.StartDate,
                    ExpireDate = model.ExpireDate,
                    Days = model.Days,
                    WeeksActive = model.WeeksActive,
                    WeeksDeactive = model.WeeksDeactive,
                    Attributes = model.Attributes,
                    Accepted = model.Accepted
                };
        }

        public List<PermissionDBModel> GetByUserId(string userId)
        {

            var model = _sqlDataAccess.LoadData<PermissionDBModel, dynamic>("spPermission_Get", new { id }, "DDB").FirstOrDefault();

            spPermission.

            if (model == null)
                return null;
            else
                return new PermissionDBModel()
                {
                    Id = model.Id,
                    WatcherID = model.WatcherID,
                    TargetID = model.TargetID,
                    StartDate = model.StartDate,
                    ExpireDate = model.ExpireDate,
                    Days = model.Days,
                    WeeksActive = model.WeeksActive,
                    WeeksDeactive = model.WeeksDeactive,
                    Attributes = model.Attributes,
                    Accepted = model.Accepted
                };



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
