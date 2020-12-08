using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
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
        private string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public PermissionController(IPermissionHandler permissionHandler) {
            _ph = permissionHandler;
        }

        [HttpGet]
        public PermissionDBModel Get(IntValue id)
        {
            var p = _ph.Get(id.Value);
            if (!(p.TargetID == UserId || p.WatcherID == UserId))
                return null;

            return p;
        }

        [HttpGet]
        [Route("ByTarget")]
        public List<PermissionDBModel> GetByTargetId()
        {
            return _ph.GetByTargetId(UserId);
        }

        [HttpGet]
        [Route("ByWatcher")]
        public List<PermissionDBModel> GetByWatcherId()
        {
            return _ph.GetByWatcherId(UserId);
        }

        [HttpGet]
        [Route("GetPendingPermissions")]
        public List<PermissionDBModel> GetPendingPermissions() {
            return _ph.GetPendingPermissions(UserId);
        }

        [HttpPut]
        public ActionResult Update(UpdatePermissionModel model)
        {
            var p = _ph.Get(model.Id);
            if (!(p.TargetID == UserId))
                return null;


            if (_ph.Update(model) == 1)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(IntValue id)
        {
            var p = _ph.Get(id.Value);
            if (!(p.TargetID == UserId || p.WatcherID == UserId))
                return Forbid();

            if (_ph.Delete(id.Value) == 1)
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
        public ActionResult AcceptPermissionRequest(IntValue id)
        {
            if (!(_ph.Get(id.Value).TargetID == UserId))
                return Forbid();

            if (_ph.AcceptPermissionRequest(id.Value) == 1)
                return Ok();
            else
                return NotFound();
        }

        [HttpPut]
        [Route("DenyPermissionReqeust")]
        public ActionResult DenyPermissionRequest(IntValue id)
        {
            var p = _ph.Get(id.Value);
            if (!(p.TargetID == UserId || p.WatcherID == UserId))
                return Forbid();
            return Delete(id);
        }
    }
}
