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
using static APIHandler.Handlers.RelayHandler;

namespace API.Controllers
{[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionHandler _ph;
        private readonly IAccountHandler _accountHandler;

        private string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public PermissionController(IPermissionHandler permissionHandler, IAccountHandler accountHandler) {
            _ph = permissionHandler;
            _accountHandler = accountHandler;
        }

        [HttpGet]
        public PermissionDBModel Get(IntValue id)
        {
            var p = _ph.Get(id.Value);
            if (p == null || !(p.TargetID == UserId || p.WatcherID == UserId))
                return null;

            return p;
        }

        [HttpGet]
        [Route("ByTarget")]
        public Permissions GetByTargetId()
        {
            return ConvertPermissions(_ph.GetByTargetId(UserId));
        }

        [HttpGet]
        [Route("ByWatcher")]
        public Permissions GetByWatcherId()
        {
            return ConvertPermissions(_ph.GetByWatcherId(UserId));
        }

        private Permissions ConvertPermissions(List<PermissionDBModel> permissions)
        {
            var data = permissions.ConvertAll(x =>
            {
                AttributeFlags attributes = (AttributeFlags)x.Attributes;
                var name = _accountHandler.GetName(UserId == x.TargetID ? x.WatcherID : x.TargetID);
                var r = new Permission()
                {
                    Id = x.Id,
                    FirstName = name.FirstName,
                    LastName = name.LastName,
                    ExpireDate = x.ExpireDate,
                    IsOwner = UserId == x.TargetID,
                    TargetId = x.TargetID,
                    Battery = attributes.HasFlag(AttributeFlags.Battery),
                    Insulin = attributes.HasFlag(AttributeFlags.Insulin),
                    BloodGlucose = attributes.HasFlag(AttributeFlags.BloodGlucose),
                };
                return r;
            });
            return new Permissions() { PermissionList = data };
        }

        [HttpGet]
        [Route("GetPendingPermissions")]
        public PermissionRequestsModel GetPendingPermissions()
        {
            var data = new PermissionRequestsModel()
            {
                Requests = _ph.GetByWatcherId(UserId).ConvertAll(x =>
                {
                    var name = _accountHandler.GetName(x.TargetID);
                    var r = new PermissionRequestModel()
                    {
                        Id = x.Id,
                        FirstName = name.FirstName,
                        LastName = name.LastName,
                    };
                    return r;
                }),
            };
            return data;
        }

        [HttpPut]
        public ActionResult Update(UpdatePermissionModel model)
        {
            var p = _ph.Get(model.Id);
            if (p == null || !(p.TargetID == UserId))
                return Forbid();


            if (_ph.Update(model) == 1)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(IntValue id)
        {
            var p = _ph.Get(id.Value);
            if (p == null || !(p.TargetID == UserId || p.WatcherID == UserId))
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
            var p = _ph.Get(id.Value);
            if (p == null || !(p.TargetID == UserId))
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
            if (p == null || !(p.TargetID == UserId || p.WatcherID == UserId))
                return Forbid();
            return Delete(id);
        }
    }
}
