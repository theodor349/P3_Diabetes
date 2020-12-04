using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Account;
using APIDataAccess.Models.Account;
using APIHandler.Handlers;
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

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAccountHandler accountHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountHandler = accountHandler;
        }

        [HttpGet]
        public AccountDBModel Get(string id)
        {
            return _accountHandler.Get(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(CreateAccountDBModel model)
        {
            // TODO: Should be moved to DataAccess
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
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
        public ActionResult UnRegister(string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("ByPhoneNumber")]
        public AccountDBModel GetByPhoneNumber(string phoneNumber)
        {
            return _accountHandler.GetByPhoneNumber(phoneNumber);
        }

        [HttpGet]
        [Route("EmailExists")]
        public bool EmailExists(string email)
        {
            return _accountHandler.EmailExists(email);
        }

        [HttpGet]
        [Route("PhoneNumberExists")]
        public bool PhoneNumberExists(string phoneNumber)
        {
            return _accountHandler.PhoneNumberExists(phoneNumber);
        }
        private bool IsValidateModel(UpdateAccountDBModel updateAccountDBModel)
        {
            if (updateAccountDBModel.ID == null && updateAccountDBModel.FirstName == null && updateAccountDBModel.LastName == null && updateAccountDBModel.PhoneNumber == null)
                return false;
            else
                return true;
        }
    }
}
