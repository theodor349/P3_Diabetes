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
        private readonly IPermissionHandler _ph;

        public PermissionController(IPermissionHandler permissionHandler) {
            _ph = permissionHandler;
        }

        [HttpGet]
        public PermissionDBModel Get(int id)
        {
            return _ph.Get(id);
        }

        [HttpGet]
        [Route("ByTarget")]
        public List<PermissionDBModel> GetByTargetId(string targetId)
        {
            return _ph.GetByTargetId(targetId);
        }

        [HttpGet]
        [Route("ByWatcher")]
        public List<PermissionDBModel> GetByWatcherId(string watcherId)
        {
            return _ph.GetByWatcherId(watcherId);
        }

        [HttpGet]
        [Route("GetPendingPermissions")]
        public List<RequestPermissionDBModel> GetPendingPermissions(string userId) {
            return _ph.GetPendingPermissions(userId);
        }

        [HttpPut]
        public ActionResult Update(UpdatePermissionModel model)
        {
            if (_ph.Update(model) == 1)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (_ph.Delete(id) == 1)
                return Ok();
            else
                return NotFound();
        }

        [HttpPost]
        [Route("RequestPermission")]
        public ActionResult RequestPermission(RequestPermissionDBModel request)
        {
            if (_ph.RequestPermission(request) == 1)
                return Ok();
            else
                return NotFound();
        }

        [HttpPut]
        [Route("AcceptPermissionRequest")]
        public ActionResult AcceptPermissionRequest(int id)
        {
            if (_ph.AcceptPermissionRequest(id) == 1)
                return Ok();
            else
                return NotFound();
        }

        [HttpPut]
        [Route("DenyPermissionReqeust")]
        public ActionResult DenyPermissionRequest(int id)
        {
            return Delete(id);
        }
    }
}
