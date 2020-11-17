using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDataAccess.Models.Relay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RelayController : ControllerBase
    {
        [HttpGet]
        public List<PumpDataPairModel> Get()
        {
            throw new NotImplementedException();
        }
    }
}
