using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Account;
using APIDataAccess.Models.Account;
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

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public AccountModel Get(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(CreateAccountModel model)
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
        public ActionResult Update(UpdateAccountModel updatedUser)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public ActionResult UnRegistre(string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("ByPhoneNumber")]
        public AccountModel GetByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("EmailExists")]
        public bool EmailExists(string email)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("PhoneNumberExists")]
        public bool PhoneNumberExists(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("NightScoutLink")]
        public string GetNightScoutLink(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("NightScoutLink")]
        public ActionResult UpdateNightScoutLink(UpdateNightScoutLinkModel input)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        [Route("UnitOfMeasurement")]
        public bool GetUnitOfMeasurement(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("UnitOfMeasurement")]
        public ActionResult UpdateUnitOfMeasurement(UpdateUnitOfMesureModel input)
        {
            throw new NotImplementedException();
        }
    }
}
