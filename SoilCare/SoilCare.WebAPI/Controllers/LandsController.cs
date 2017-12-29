using SoilCare.WebAPI.Data;
using SoilCare.WebAPI.Models;
using System;
using AutoMapper;
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
        // Get: api/Lands/landId/Measures
        [Route("api/Lands/{id}/Measures")]
        public IHttpActionResult GetMeasuresByLandId(string id)
        {
            IList<MeasureModel> measures = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                measures = db.Measures.Where(s => s.Land_id.Equals(id))
                                   .Select(AutoMapper.Mapper.Map<Measure, MeasureModel>)
                                   .ToList();
            }
            if (measures.Count == 0) return NotFound();
            return Ok(measures);
        }
        // Get: api/Lands/id
        public IHttpActionResult GetLandById(string id)
        {
            LandModel land = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                land = db.Lands.Include("Measures")
                                   .Where(s => s.Land_id.Equals(id))
                                   .Select(AutoMapper.Mapper.Map<Land, LandModel>)
                                   .FirstOrDefault();
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

            Land _land = Mapper.Map<AddLandModel, Land>(land);
            _land.Land_id = Guid.NewGuid().ToString("N");
            _land.Created_at = DateTime.Now;
            _land.Status = "Active";
            
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
