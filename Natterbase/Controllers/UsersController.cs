using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;
using Natterbase.Models;
using Natterbase.Utility;

namespace Natterbase.Controllers
{
    public class UsersController : ApiController
    {
        private NatterbaseEntities db = new NatterbaseEntities();
        

        // POST: api/Users
        [Route("signup")]
        [ResponseType(typeof(user))]
        public IHttpActionResult Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                LogAudit.Log("Postuser", user.username, 0);
                return BadRequest(ModelState);
            }
            else
            {
                string salt = App.CreateSalt();
                user.password = App.CreatePasswordHash(user.password, salt);
                db.users.Add(user);
                db.SaveChanges();

                LogAudit.Log("Postuser", user.username, 1);

                return Ok(user);
            }

            
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(int id)
        {
            return db.users.Count(e => e.id == id) > 0;
        }
        
    }
}