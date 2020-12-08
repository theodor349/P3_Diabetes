﻿using System;
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
        public string GetNightscoutLink(string id) {
            return _accountHandler.GetNightscoutLink(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(InputCreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                if(await _accountHandler.RegisterAccount(model))
                    return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public ActionResult Update(UpdateAccountDBModel updatedUser)
        {
            if (IsValidateModel(updatedUser))
            {
                _accountHandler.UpdateAccount(updatedUser);
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
        [Route("ByPhoneNumber")]
        public AccountDBModel GetByPhoneNumber(StringValue phoneNumber)
        {
            return _accountHandler.GetByPhoneNumber(phoneNumber.Value);
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

        [HttpPut]
        [Route("UpdateNightScoutLink")]
        public ActionResult UpdateNightScoutLink(StringValue url)
        {
            if (_relayHandler.ConnectionOk(url.Value))
            {
                _accountHandler.UpdateNightScoutLink(new UpdateNightScoutLinkModel() { Id = UserId, NewLink = url.Value});
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private bool IsValidateModel(UpdateAccountDBModel updateAccountDBModel)
        {
            if (updateAccountDBModel.Id == null && updateAccountDBModel.FirstName == null && updateAccountDBModel.LastName == null && updateAccountDBModel.PhoneNumber == null)
                return false;
            else
                return true;
        }
    }
}