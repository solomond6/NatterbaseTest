using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Natterbase.Models;
using Natterbase.Utility;

namespace Natterbase.Controllers
{
    public class CountriesController : ApiController
    {
        private NatterbaseEntities db = new NatterbaseEntities();

        // GET: api/Countries
        public IQueryable<country> Getcountries()
        {
            LogAudit.Log("Getcountries", "", 0);
            return db.countries;
        }

        // GET: api/Countries/5
        [ResponseType(typeof(country))]
        public IHttpActionResult Getcountry(int id)
        {
            country country = db.countries.Find(id);
            if (country == null)
            {
                LogAudit.Log("Getcountry", "", 0);
                return NotFound();
            }

            LogAudit.Log("Getcountry", "", 1);
            return Ok(country);
        }

        // PUT: api/Countries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcountry(int id, country country)
        {
            string token = Request.Headers.GetValues("token").First();

            if (token == null | token == "")
            {
                LogAudit.Log("Putcountry", "", 0);
                return Content(HttpStatusCode.BadRequest, new
                {
                    Succeeded = false,
                    Message = "Invalid token"
                });
            }
            else
            {
                string validateToken = TokenManager.ValidateToken(token);

                if (validateToken == null)
                {
                    LogAudit.Log("Putcountry", validateToken, 0);
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Succeeded = false,
                        Message = "Invalid token"
                    });
                }

                if (!ModelState.IsValid)
                {
                    LogAudit.Log("Putcountry", validateToken, 0);
                    return BadRequest(ModelState);
                }

                if (id != country.id)
                {
                    LogAudit.Log("Putcountry", validateToken, 0);
                    return BadRequest();
                }

                db.Entry(country).State = EntityState.Modified;

                try
                {
                    LogAudit.Log("Putcountry", validateToken, 1);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!countryExists(id))
                    {
                        LogAudit.Log("Putcountry", validateToken, 0);
                        return NotFound();
                    }
                    else
                    {
                        LogAudit.Log("Putcountry", validateToken, 0);
                        return NotFound();

                    }
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/Countries
        [ResponseType(typeof(country))]
        public IHttpActionResult Postcountry(country country)
        {
            string token = Request.Headers.GetValues("token").First();

            if (token == null | token == "")
            {
                LogAudit.Log("Postcountry", "", 0);
                return Content(HttpStatusCode.BadRequest, new
                {
                    Succeeded = false,
                    Message = "Invalid token"
                });
            }
            else
            {
                string validateToken = TokenManager.ValidateToken(token);

                if (validateToken == null)
                {
                    LogAudit.Log("Postcountry", validateToken, 0);
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Succeeded = false,
                        Message = "Invalid token"
                    });
                }

                if (!ModelState.IsValid)
                {
                    LogAudit.Log("Postcountry", validateToken, 0);
                    return BadRequest(ModelState);
                }
                else
                {
                    db.countries.Add(country);
                    db.SaveChanges();
                    LogAudit.Log("Postcountry", validateToken, 1);
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Succeeded = true,
                        Message = "Country Created Successfully"
                    });
                }

            }
        }

        // DELETE: api/Countries/5
        [ResponseType(typeof(country))]
        public IHttpActionResult Deletecountry(int id)
        {
            string token = Request.Headers.GetValues("token").First();

            if (token == null | token == "")
            {
                LogAudit.Log("Deletecountry", "", 0);
                return Content(HttpStatusCode.BadRequest, new
                {
                    Succeeded = false,
                    Message = "Invalid token"
                });
            }
            else
            {
                string validateToken = TokenManager.ValidateToken(token);

                country country = db.countries.Find(id);
                if (country == null)
                {
                    LogAudit.Log("Deletecountry", validateToken, 0);

                    return NotFound();
                }

                db.countries.Remove(country);
                db.SaveChanges();

                LogAudit.Log("Deletecountry", validateToken, 1);

                return Ok(country);
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

        private bool countryExists(int id)
        {
            return db.countries.Count(e => e.id == id) > 0;
        }
    }
}