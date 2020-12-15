using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using APIDataAccess.Models.Account;
using APIHandler.Handlers;
using APIHandler.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// The API is in charge of validating input and passing it down to the handler if input is valid
/// </summary>
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAccountHandler _accountHandler;
        private readonly IRelayHandler _relayHandler;

        private string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAccountHandler accountHandler, IRelayHandler relayHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountHandler = accountHandler;
            _relayHandler = relayHandler;
        }

        [HttpGet]
        public AccountDBModel Get()
        {
            return _accountHandler.Get(UserId);
        }

        [HttpGet]
        [Route("GetName")]
        public AccountNameModel GetName(StringValue id)
        {
            return _accountHandler.GetName(id.Value);
        }

        [HttpGet]
        [Route("GetNightscoutLink")]
        public string GetNightscoutLink() {
            return _accountHandler.GetNightscoutLink(UserId);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(InputCreateAccountModel model)
        {
            if (_accountHandler.EmailExists(model.Email))
                return BadRequest();
            if (_accountHandler.PhoneNumberExists(model.PhoneNumber))
                return BadRequest();
            if(!string.IsNullOrWhiteSpace(model.NSLink) && !_relayHandler.ConnectionOk(model.NSLink))
                return BadRequest();

            if (ModelState.IsValid)
            {
                if(!_relayHandler.ConnectionOk(model.NSLink))
                    return BadRequest();

                if (await _accountHandler.RegisterAccount(model))
                    return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public ActionResult Update(AccountUpdate update)
        {
            UpdateAccountDBModel updatedUser = ConvertAccountUpdate(update);
            if (IsValidateModel(updatedUser))
            {
                if(_accountHandler.UpdateAccount(updatedUser) == 1)
                    return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public ActionResult UnRegister(StringValue id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("ByPhoneNumber/{phoneNumber}")]
        public PublicAccountModel GetByPhoneNumber(string phoneNumber)
        {
            var a = _accountHandler.GetByPhoneNumber(phoneNumber);
            return new PublicAccountModel()
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
            };
        }

        [HttpGet]
        [Route("EmailExists")]
        public bool EmailExists(StringValue email)
        {
            return _accountHandler.EmailExists(email.Value);
        }

        [HttpGet]
        [Route("PhoneNumberExists")]
        public bool PhoneNumberExists(StringValue phoneNumber)
        {
            return _accountHandler.PhoneNumberExists(phoneNumber.Value);
        }

        [HttpPost]
        [Route("TestNightScoutLink")]
        public bool TestNightScoutLink(StringValue link) {
            return _relayHandler.ConnectionOk(link.Value);
        }

        [HttpPut]
        [Route("UpdateNightScoutLink")]
        public ActionResult UpdateNightScoutLink(StringValue url)
        {
            if (!string.IsNullOrWhiteSpace(url.Value) && _relayHandler.ConnectionOk(url.Value))
            {
                _accountHandler.UpdateNightScoutLink(new UpdateNightScoutLinkModel() { Id = UserId, NewLink = url.Value});
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private bool IsValidateModel(UpdateAccountDBModel update)
        {
            if (update.Id == null || update.FirstName == null || update.LastName == null)
                return false;
            else
                return true;
        }

        private UpdateAccountDBModel ConvertAccountUpdate(AccountUpdate update) {
            return new UpdateAccountDBModel {
                Id = UserId,
                FirstName = update.FirstName,
                LastName = update.LastName,
                IsEU = update.IsEU
            };
        }
    }
}