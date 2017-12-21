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
using SoilCare.WebAPI.Data;

namespace SoilCare.WebAPI.Controllers
{
    public class MicrosoftsController : ApiController
    {
        private SoilCareEntities db = new SoilCareEntities();

        // GET: api/Microsofts
        public IQueryable<Plant> GetPlants()
        {
            return db.Plants;
        }

        // GET: api/Microsofts/5
        [ResponseType(typeof(Plant))]
        public IHttpActionResult GetPlant(string id)
        {
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return NotFound();
            }

            return Ok(plant);
        }

        // PUT: api/Microsofts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlant(string id, Plant plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plant.Plant_id)
            {
                return BadRequest();
            }

            db.Entry(plant).State = EntityState.Modified;
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Microsofts
        [ResponseType(typeof(Plant))]
        public IHttpActionResult PostPlant(Plant plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Plants.Add(plant);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PlantExists(plant.Plant_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = plant.Plant_id }, plant);
        }

        // DELETE: api/Microsofts/5
        [ResponseType(typeof(Plant))]
        public IHttpActionResult DeletePlant(string id)
        {
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return NotFound();
            }

            db.Plants.Remove(plant);
            db.SaveChanges();

            return Ok(plant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlantExists(string id)
        {
            return db.Plants.Count(e => e.Plant_id == id) > 0;
        }
    }
}