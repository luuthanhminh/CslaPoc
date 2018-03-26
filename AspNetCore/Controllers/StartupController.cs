using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Controllers
{
    [Route("api")]
    public class StartupController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("I'm fine");
        }
    }
}
