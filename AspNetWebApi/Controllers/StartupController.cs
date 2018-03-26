using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    [RoutePrefix("api")]
    public class StartupController : ApiController
    {
        #region GET /api
        [HttpGet]
        [Route]
        public IHttpActionResult PingServer()
        {
            return Ok("I'm fine");
        }
        #endregion
    }
}