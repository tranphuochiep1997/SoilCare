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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

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
        // Get code using telephone
        [Route("api/Users/GetCode/{id}")]
        public IHttpActionResult GetCodeByTelephone(string id)
        {
            Random rand = new Random();
            string countryCode = "+84";
            string telephone = id.Substring(1);

            const string accountSid = "ACb4103bcbc0a10f7941342a3f86191bfe";
            const string authToken = "6aa84952874c9adeffe542bcae543cfb";
            TwilioClient.Init(accountSid, authToken);

            string verified_code = rand.Next(1000000).ToString("D6");

            var to = new PhoneNumber(countryCode + telephone);
            string _body = "Your verify code is "
                + verified_code + ". Please verify within 5 minutes";

            var message = MessageResource.Create(
                to,
                from: new PhoneNumber("+17062223527 "),
                body: _body);

            return Ok(new {
                Verified_code = verified_code,
                Expiration_time = DateTime.Now.AddMinutes(5),
            });
        }

        [Route("api/Users/telephone/{id}")]
        public IHttpActionResult GetUserByTelephone(string id)
        {
            User user = null;
            bool IsNew = false;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                user = db.Users.FirstOrDefault(s => s.User_telephone.Equals(id));
            }
            if (user == null)
            {
                user = PostUserByTelephone(id);
                if (user == null) return Conflict();
                IsNew = true;
            }
            return Ok(new {
                user.User_id,
                IsNew,
            });
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
        // POST telephone first: api/Users
        public User PostUserByTelephone(string telephone)
        {
            User _user = new User();
            _user.User_id = Guid.NewGuid().ToString("N");
            _user.Created_at = DateTime.Now;
            _user.User_telephone = telephone;

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
                        return null;
                    }
                    throw;
                }
            }
            return _user;
        }
        // PUT: api/Users/UserId
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, AddUserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User _user = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                _user = db.Users.Find(id);

                if (_user == null) return NotFound();

                // Map model to entity
                Mapper.Map<AddUserModel, User>(user, _user);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return Ok();
        }
        private bool UserExists(SoilCareEntities db, string id)
        {
            return db.Users.Count(e => e.User_id.Equals(id)) > 0;
        }
    }
}