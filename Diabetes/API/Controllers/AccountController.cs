using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public string Get(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Registre(AccountModel user)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public ActionResult Update(AccountModel updatedUser)
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
        public bool GetByPhoneNumber(string phoneNumber)
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
        [Route("Name")]
        public string GetName(string id)
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
