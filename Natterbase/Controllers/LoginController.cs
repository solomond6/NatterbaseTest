using Natterbase.Models;
using Natterbase.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FluentValidation;

namespace Natterbase.Controllers
{
    public class LoginController : ApiController
    {

        [Route("login")]
        public IHttpActionResult Login(login u)
        {
            if (ModelState.IsValid)
            {
                using (NatterbaseEntities db = new NatterbaseEntities())
                {

                    string salt = App.CreateSalt();
                    u.password = App.CreatePasswordHash(u.password, salt);
                    //un-hash password?
                    var v = db.users.Where(a => a.username.Equals(u.username) && a.password.Equals(u.password)).FirstOrDefault();
                    if (v != null)
                    {
                        return Content(HttpStatusCode.OK, new
                        {
                            Succeeded = true,
                            token = TokenManager.GenerateToken(u.username)
                            
                        });

                    }
                    else
                    {
                        //return NotFound();
                        return Content(HttpStatusCode.NotFound, new
                        {
                            Succeeded = false,
                            Message = "Invalid credentials"
                            
                        });
                    }
                }
            }
            return BadRequest();
        }
    }
}
