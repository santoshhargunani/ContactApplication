using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactAppService;
using ContactAppService.Abstract;
using ContactAppService.Concrete;

namespace ContactAppService.Controllers
{
    public class ContactController : ApiController
    {
        private IRepository<Contact> objContact = null;
        public ContactController()
        {
            this.objContact = new Repository<Contact>();
        }

        [Route("api/contacts/Get")]
        public IHttpActionResult GetContacts()
        {

            if (objContact.GetAll().Any())
                return Ok(objContact.GetAll());
            else
                return NotFound();
        }

        [Route("api/Contact/GetContact")]
        public Contact GetContact(int id)
        {
            return objContact.GetById(id);
        }

        [Route("Api/Contact/PostContact")]
        public IHttpActionResult PostContact(ContactAppService.Contact Contact)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Contact data");
            if (objContact.Insert(Contact).ID > 0)
                return Ok();
            else
                return BadRequest("Bad request");

        }

        [Route("Api/Contact/Put")]
        public IHttpActionResult Put(ContactAppService.Contact contact)
        {

            objContact.Update(contact);
            return Ok();
        }

        [Route("Api/Contact/Delete")]
        public IHttpActionResult Delete(int id)
        {
            Contact objcon = GetContact(id);
            objContact.Delete(objcon);
            return Ok();
        }
    }
}
