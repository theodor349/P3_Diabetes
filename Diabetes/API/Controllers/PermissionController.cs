using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDataAccess.Models.Permission;
using APIHandler.Handlers;
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
        private readonly IPermissionHandler permissionHandler;

        public PermissionController(IPermissionHandler permissionHandler) {
            this.permissionHandler = permissionHandler;
        }

        [HttpGet]
        public UpdatePermissionDBModel Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("User")]
        public List<UpdatePermissionDBModel> GetByTargetId(string targetId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("User")]
        public List<UpdatePermissionDBModel> GetByWatcherId(string watcherId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public ActionResult Update(UpdatePermissionDBModel updatedPermission)
        {
            if (permissionHandler.Update(updatedPermission) == 1)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (permissionHandler.Delete(id) == 1)
                return Ok();
            else
                return NotFound();
        }

        [HttpPost]
        [Route("RequestPermission")]
        public ActionResult RequestPermission(RequestPermissionDBModel request)
        {
            //todo: verify request

            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("AcceptPermissionRequest")]
        public ActionResult AcceptPermissionRequest(int id)
        {
            if (permissionHandler.AcceptPermissionRequest(id) == 1)
                return Ok();
            else
                return NotFound();
        }

        [HttpPost]
        [Route("DenyPermissionReqeust")]
        public ActionResult DenyPermissionRequest(int id)
        {
            if (permissionHandler.DenyPermissionRequest(id) == 1)
                return Ok();
            else
                return NotFound();

        }

        [HttpGet]
        [Route("GetPendingPermissions")]
        public List<UpdatePermissionDBModel> GetPendingPermissions()
        {
            throw new NotImplementedException();
        }

        bool IsValidUser(string id) {
            //valid user id: "2"
            //invalid user id: "sdf"

            throw new NotImplementedException();
        }

        bool IsValidPermissionUpdate(UpdatePermissionDBModel update) {
            throw new NotImplementedException();
        }

        bool IsValidPermissionRequest(RequestPermissionDBModel request) {
            throw new NotImplementedException();
        }
    }
}
