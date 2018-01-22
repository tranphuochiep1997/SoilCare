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
using AutoMapper;
using SoilCareWebAPI.Data;
using SoilCareWebAPI.Models;

namespace SoilCareWebAPI.Controllers
{
    public class UsersController : ApiController
    {

        [Route("api/Users/{id}/Lands")]
        public IHttpActionResult GetLandsByUserId(string id)
        {
            IList<LandModel> listLands = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                listLands = db.Lands.Where(s => s.User_id.Equals(id))
                                    .Select(AutoMapper.Mapper.Map<Land, LandModel>)
                                    .ToList();
            }
            if (listLands.Count == 0) return NotFound();
            return Ok(listLands);
        }
        [Route("api/Users/telephone/{id}")]
        public IHttpActionResult GetUserByTelephone(string id)
        {
            //User user = null;
            //using (SoilCareEntities db = new SoilCareEntities())
            //{
            //    user = db.Users.FirstOrDefault(s => s.Telephone.Equals(id));
            //}
            //if (user == null) return NotFound();
            //return Ok(user.User_id);
            return Ok(2018);
        }
        // Get: api/Users/id
        public IHttpActionResult GetUserById(string id)
        {
            UserModel user = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                user = db.Users.Include("Lands")
                               .Where(s => s.User_id.Equals(id))
                               .Select(AutoMapper.Mapper.Map<User, UserModel>)
                               .FirstOrDefault();
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

            User _user = Mapper.Map<AddUserModel, User>(user);
            _user.User_id = Guid.NewGuid().ToString("N");
            _user.Created_at = DateTime.Now;

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