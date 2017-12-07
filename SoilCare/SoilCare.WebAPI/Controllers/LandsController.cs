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
    public class LandsController : ApiController
    {
        // Get: api/Lands/id
        public IHttpActionResult GetLand(string id)
        {
            LandViewModel land = new LandViewModel();
            using (SoilCareEntities db = new SoilCareEntities())
            {
                land = db.UserLands.Where(s => s.Land_id.Equals(id))
                                   .Where(s => s.Status.Equals("Active"))
                                   .Select(s => new LandViewModel
                                   {
                                       Land_id = s.Land_id,
                                       Land_name = s.Land_name,
                                       Land_address = s.Land_address,
                                       Created_at = (DateTime)s.Created_at,
                                   }).FirstOrDefault();
            }
            if (land == null) return NotFound();
            return Ok(land);
        }
    }
}
