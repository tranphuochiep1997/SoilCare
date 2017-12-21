
using SoiCare.API.Models;
using SoilCare.WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SoiCare.API.Controllers
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
                                     .Select(s => new PlantModel()
                                     {
                                         Plant_id = s.Plant_id,
                                         Plant_name = s.Plant_name,
                                         Plant_image = s.Plant_image,
                                         Plant_discription = s.Plant_discription,
                                         Soil_id = s.Soil_id,
                                     }).ToList();
            }
            return plantList;
        }

        // Get: api/Plants/id
        public IHttpActionResult GetPlantById(string id)
        {
            PlantModel plant = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                plant = db.Plants.Where(s => s.Plant_id.Equals(id))
                             .Select(s => new PlantModel
                             {
                                 Plant_id = s.Plant_id,
                                 Plant_name = s.Plant_name,
                                 Plant_image = s.Plant_image,
                                 Plant_discription = s.Plant_discription,
                                 Soil_id = s.Soil_id,
                                 Soil = new SoilModel()
                                 {
                                     Soil_id = s.Soil.Soil_id,
                                     Soil_name = s.Soil.Soil_name,
                                     Min_acidity = (double)s.Soil.Min_acidity,
                                     Max_acidity = (double)s.Soil.Max_acidity,
                                     Min_nutrient = (double)s.Soil.Min_nutrient,
                                     Max_nutrient = (double)s.Soil.Max_nutrient,
                                     Min_humidity = (double)s.Soil.Min_humidity,
                                     Max_humidity = (double)s.Soil.Max_humidity,
                                     Min_water_retention = (double)s.Soil.Min_water_retention,
                                     Max_water_retention = (double)s.Soil.Max_water_retention,
                                     Min_porosity = (double)s.Soil.Min_porosity,
                                     Max_porosity = (double)s.Soil.Max_porosity,
                                     Min_salinity = (double)s.Soil.Min_salinity,
                                     Max_salinity = (double)s.Soil.Max_salinity,
                                 },
                             }).FirstOrDefault<PlantModel>();
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
            Plant _plant = new Plant
            {
                Plant_id = initId,
                Plant_name = plant.Plant_name,
                Plant_image = plant.Plant_image,
                Plant_discription = plant.Plant_discription,
                Status = "Pending",
                Soil_id = null,
                Soil = null,
            };
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
