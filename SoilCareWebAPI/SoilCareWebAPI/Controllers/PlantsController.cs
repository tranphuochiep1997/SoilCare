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
    public class PlantsController : ApiController
    {
        // Get: api/Plants
        public IList<PlantModel> GetAllPlants()
        {
            IList<PlantModel> plantList = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                plantList = db.Plants.Where(s => s.Status.ToLower().Equals("approved"))
                                     .Select(AutoMapper.Mapper.Map<Plant, PlantModel>)
                                     .ToList();
            }
            return plantList;
        }

        // Get: api/Plants/id
        public IHttpActionResult GetPlantById(string id)
        {
            PlantModel plant = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                plant = db.Plants.Include("Soil").Where(s => s.Plant_id.Equals(id))
                             .Select(AutoMapper.Mapper.Map<Plant, PlantModel>)
                             .FirstOrDefault<PlantModel>();
            }
            if (plant == null) return NotFound();
            return Ok(plant);
        }
        // POST: api/Plants
        [ResponseType(typeof(Plant))]
        public IHttpActionResult PostPlant(AddPlantModel plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string initId = Guid.NewGuid().ToString("N");
            Plant _plant = Mapper.Map<AddPlantModel, Plant>(plant);
            _plant.Plant_id = initId;
            _plant.Status = "Pending";

            using (SoilCareEntities db = new SoilCareEntities())
            {
                db.Plants.Add(_plant);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (PlantExists(db, _plant.Plant_id))
                    {
                        return Conflict();
                    }
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = _plant.Plant_id }, _plant);
        }
        private bool PlantExists(SoilCareEntities db, string id)
        {
            return db.Plants.Count(e => e.Plant_id.Equals(id)) > 0;
        }
    }
}