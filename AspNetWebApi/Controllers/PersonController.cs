using CslaPoc.Core.Business.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    [RoutePrefix("api/persons")]
    public class PersonController : ApiController
    {
        #region GET /api/persons
        [HttpGet]
        [Route]
        public IHttpActionResult GetList()
        {
            var persons = PersonList.GetList();
            return Ok(persons);
        }
        #endregion

        #region GET /api/persons/id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var person = PersonEdit.GetPersonEdit(id);
            return Ok(person);
        }
        #endregion
    }
}