using SoilCare.WebAPI.Data;
using SoilCare.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SoilCare.WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        public IHttpActionResult GetUser(string id)
        {
            UserViewModel user = new UserViewModel();
            using (SoilCareEntities db = new SoilCareEntities())
            {
                user = db.Users.Where(s => s.User_id.Equals(id))
                               .Select(s => new UserViewModel
                               {
                                   User_id = s.User_id,
                                   User_name = s.User_name,
                                   Telephone = s.Telephone,
                                   Region = s.Region,
                                   Created_at = (DateTime)s.Created_at,
                               }).FirstOrDefault<UserViewModel>();
            }
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
