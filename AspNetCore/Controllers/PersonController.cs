using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Controllers
{
    [Route("api/persons")]
    public class PersonController : Controller
    {
        #region GET /api/persons
        [HttpGet]
        [Route("")]
        public IActionResult GetList()
        {
            return Ok("persons");
        }
        #endregion

        #region GET /api/persons/id
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok("person");
        }
        #endregion
    }
}
