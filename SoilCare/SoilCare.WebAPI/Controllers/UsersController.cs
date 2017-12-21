using SoilCare.WebAPI.Data;
using SoilCare.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SoilCare.WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/Users/telephone/{telephone}")]
        public IHttpActionResult GetUserByTelephone(string telephone)
        {
            UserModel user = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                user = db.Users.Where(s => s.Telephone.Equals(telephone))
                               .Select(s => new UserModel
                               {
                                   User_id = s.User_id,
                                   User_name = s.User_name,
                                   User_image = s.User_image,
                                   Telephone = s.Telephone,
                                   Region = s.Region,
                                   Created_at = s.Created_at,
                               }).FirstOrDefault<UserModel>();
            }
            if (user == null) return NotFound();
            return Ok(user);
        }
        // Get: api/Users/id
        public IHttpActionResult GetUserById(string id)
        {
            UserModel user = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                user = db.Users.Include("Lands")
                               .Where(s => s.User_id.Equals(id))
                               .Select(s => new UserModel
                               {
                                   User_id = s.User_id,
                                   User_name = s.User_name,
                                   User_image = s.User_image,
                                   Telephone = s.Telephone,
                                   Region = s.Region,
                                   Created_at = s.Created_at,
                                   Lands = s.Lands.Where(l => l.Status.Equals("Active"))
                                                      .Select(l => new LandModel
                                                      {
                                                          Land_id = l.Land_id,
                                                          Land_name = l.Land_name,
                                                          Land_address = l.Land_address,
                                                          Land_image = l.Land_image,
                                                          Created_at = l.Created_at,
                                                      }).ToList(),
                               }).FirstOrDefault<UserModel>();
            }
            if (user == null) return NotFound();
            return Ok(user);
        }
        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(AddUserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string initId = Guid.NewGuid().ToString("N");
            User _user = new User
            {
                User_id = initId,
                User_name = user.User_name,
                User_image = user.User_image,
                Telephone = user.Telephone,
                Region = user.Region,
                Created_at = DateTime.Now,
            };
            using (SoilCareEntities db = new SoilCareEntities())
            {
                db.Users.Add(_user);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (UserExists(db, _user.User_id))
                    {
                        return Conflict();
                    }
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = _user.User_id }, _user);
        }
        private bool UserExists(SoilCareEntities db, string id)
        {
            return db.Users.Count(e => e.User_id.Equals(id)) > 0;
        }
    }
}
