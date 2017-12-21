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
    public class LandsController : ApiController
    {
        // Get: api/Lands/id
        public IHttpActionResult GetLandById(string id)
        {
            LandModel land = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                land = db.Lands.Include("Measurements")
                                   .Where(s => s.Land_id.Equals(id))
                                   .Select(s => new LandModel
                                   {
                                       Land_id = s.Land_id,
                                       Land_name = s.Land_name,
                                       Land_address = s.Land_address,
                                       Land_image = s.Land_image,
                                       Created_at = s.Created_at,
                                       Measures = s.Measurements.Select(m =>
                                       new MeasureModel
                                       {
                                           Measure_id = m.Measure_id,
                                           Created_at = m.Created_at,
                                           Plant_name = m.Plant.Plant_name,
                                           HasSolution = m.SolutionOffers.Count > 0,
                                           Rate = m.Rate,
                                       }).OrderBy(m => m.Created_at).ToList(),
                                   }).FirstOrDefault();
            }
            if (land == null) return NotFound();
            return Ok(land);
        }
        // POST: api/Lands
        [ResponseType(typeof(Land))]
        public IHttpActionResult PostLand(AddLandModel land)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string initId = Guid.NewGuid().ToString("N");
            Land _land = new Land
            {
                Land_id = initId,
                Land_name = land.Land_name,
                Land_address = land.Land_address,
                Land_image = land.Land_image,
                User_id = land.User_id,
                Created_at = DateTime.Now,
                Status = "Active",
            };
            
            using (SoilCareEntities db = new SoilCareEntities())
            {
                db.Lands.Add(_land);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (LandExists(db, _land.Land_id))
                    {
                        return Conflict();
                    }
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = _land.Land_id }, _land);
        }
        // PUT: api/Lands/landId
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLand(string id, AddLandModel land)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Land _land = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                _land = db.Lands.Find(id);

                if (_land == null) return NotFound();

                // Map model to entity
                _land.Land_name = land.Land_name;
                _land.Land_image = land.Land_image;
                _land.Land_address = land.Land_address;

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
        // Delete: api/Lands/landId
        public IHttpActionResult DeleteLand(string id)
        {
            Land _land = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                _land = db.Lands.Find(id);
                if (_land == null)
                {
                    db.Dispose();
                    return NotFound();
                }
                _land.Status = "Deleted";
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
            return Ok();
        }
        private bool LandExists(SoilCareEntities db, string id)
        {
            return db.Lands.Count(e => e.Land_id.Equals(id)) > 0;
        }
    }
}
