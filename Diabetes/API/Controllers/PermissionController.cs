using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDataAccess.Models.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        [HttpGet]
        public PermissionDBModel Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("User")]
        public List<PermissionDBModel> GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public ActionResult Update(UpdatePermissionDBModel updatedPermission)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("RequestPermission")]
        public ActionResult RequestPermission(RequestPermissionDBModel request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("AcceptPermissionRequest")]
        public ActionResult AcceptPermissionRequest(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("DenyPermissionReqeust")]
        public ActionResult DenyPermissionReqeust(string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetPendingPermissions")]
        public List<PermissionDBModel> GetPendingPermissions()
        {
            throw new NotImplementedException();
        }
    }
}
