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
{[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionHandler permissionHandler;

        public PermissionController(IPermissionHandler permissionHandler) {
            this.permissionHandler = permissionHandler;
        }

        [HttpGet]
        public PermissionDBModel Get(int id)
        {
            return permissionHandler.Get(id);
        }

        [HttpGet]
        [Route("User")]
        public List<PermissionDBModel> GetByTargetId(string targetId)
        {
            return permissionHandler.GetByTargetId(targetId);
        }

        [HttpGet]
        [Route("User")]
        public List<PermissionDBModel> GetByWatcherId(string watcherId)
        {
            return permissionHandler.GetByWatcherId(watcherId);
        }

        [HttpGet]
        [Route("GetPendingPermissions")]
        public List<RequestPermissionDBModel> GetPendingPermissions(string userId) {
            return permissionHandler.GetPendingPermissions(userId);
        }

        [HttpPut]
        public ActionResult Update(UpdatePermissionModel model)
        {
            if (permissionHandler.Update(model) == 1)
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
            if (permissionHandler.RequestPermission(request) == 1)
                return Ok();
            else
                return NotFound();
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
            return Delete(id);
        }
    }
}
