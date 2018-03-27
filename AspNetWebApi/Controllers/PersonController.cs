using CslaPoc.Core.Business.Person;
using CslaPoc.Core.Models.BindingModels;
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
            var persons = PersonList.GetList().Select(ps => new
            {
                Id = ps.Id,
                Name = ps.Name
            });
            return Ok(persons);
        }
        #endregion

        #region GET /api/persons/id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var person = PersonEdit.GetPersonEdit(id);
            return Ok(new {
                Id = person.Id,
                Name = person.Name
            });
        }
        #endregion

        #region POST /api/persons
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreatePeson([FromBody] PersonBindingModel model)
        {
            var person = PersonEdit.CreatePerson();

            person.Name = model.Name;

            if(person.IsSavable)
            {
                person = person.Save();
                return Ok(new
                {
                    Id = person.Id,
                    Name = person.Name
                });
            }else
            {
                var error = String.Empty;
                foreach (var item in person.BrokenRulesCollection)
                    error += item.Description + Environment.NewLine;

                return BadRequest(error);
            }
           
        }
        #endregion

        #region DELETE /api/persons/id
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeletePerson(int id)
        {
            PersonEdit.DeletePersonEdit(id);

            try
            {
                var person = Csla.DataPortal.Fetch<PersonEdit>(id);
                return BadRequest("Person NOT deleted");
            }
            catch
            {
                return Ok("Person successfully deleted");
            };
        }
        #endregion
    }
}