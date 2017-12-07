
using SoiCare.API.Models;
using SoilCare.WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SoiCare.API.Controllers
{
    public class PlantsController : ApiController
    {

        // Get: api/Plants
        public IHttpActionResult GetAllPlants()
        {
            List<PlantViewModel> plantList = new List<PlantViewModel>();
            using (SoilCareEntities db = new SoilCareEntities())
            {

                plantList = db.Plants.Where(s => s.Status.Equals("Approved"))
                                     .Select(s => new PlantViewModel()
                                     {
                                         Plant_id = s.Plant_id,
                                         Plant_name = s.Plant_name,
                                         Plant_image = s.Plant_image,
                                         Plant_discription = s.Plant_discription,
                                         Soil_id = s.Soil_id,

                                     }).ToList();
            }
            

            if (plantList.Count == 0) return NotFound();
            return Ok(plantList);
        }

        // Get: api/Plants/id
        public IHttpActionResult GetPlant(string id)
        {
            PlantViewModel plant = new PlantViewModel();
            using (SoilCareEntities db = new SoilCareEntities())
            {
                plant = db.Plants.Include("Soil").Where(s => s.Plant_id.Equals(id))
                             .Select(s => new PlantViewModel
                             {
                                 Plant_id = s.Plant_id,
                                 Plant_name = s.Plant_name,
                                 Plant_image = s.Plant_image,
                                 Plant_discription = s.Plant_discription,
                                 Soil_id = s.Soil_id,
                                 Soil = new SoilViewModel()
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
                             }).FirstOrDefault<PlantViewModel>();
            }
            

            if (plant == null) return NotFound();
            return Ok(plant);
        }
    }
}
