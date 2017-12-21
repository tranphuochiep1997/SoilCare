using SoiCare.API.Models;
using SoilCare.WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SoilCare.WebAPI.Controllers
{
    public class SoilsController : ApiController
    {
        // Get: api/Soils
        public IHttpActionResult GetAllSoils()
        {
            IList<SoilModel> listSoil = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                listSoil = db.Soils.Select(s => new SoilModel
                {
                    Soil_id = s.Soil_id,
                    Soil_name = s.Soil_name,
                    Min_nutrient = s.Min_nutrient,
                    Max_nutrient = s.Max_nutrient,
                    Min_acidity = s.Min_acidity,
                    Max_acidity = s.Max_acidity,
                    Min_humidity = s.Min_humidity,
                    Max_humidity = s.Max_humidity,
                    Min_porosity = s.Min_porosity,
                    Max_porosity = s.Max_porosity,
                    Min_salinity = s.Min_salinity,
                    Max_salinity = s.Max_salinity,
                    Min_water_retention = s.Min_water_retention,
                    Max_water_retention = s.Max_water_retention,
                }).ToList();
            }
            if (listSoil.Count == 0) return NotFound();
            return Ok(listSoil);
        }
        // Get: api/Soils/id
        public IHttpActionResult GetSoilById(string id)
        {
            SoilModel Soil = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                Soil = db.Soils.Include("Plants")
                                   .Where(s => s.Soil_id.Equals(id))
                                   .Select(s => new SoilModel
                                   {
                                       Soil_id = s.Soil_id,
                                       Soil_name = s.Soil_name,
                                       Min_nutrient = s.Min_nutrient,
                                       Max_nutrient = s.Max_nutrient,
                                       Min_acidity = s.Min_acidity,
                                       Max_acidity = s.Max_acidity,
                                       Min_humidity = s.Min_humidity,
                                       Max_humidity = s.Max_humidity,
                                       Min_porosity = s.Min_porosity,
                                       Max_porosity = s.Max_porosity,
                                       Min_salinity = s.Min_salinity,
                                       Max_salinity = s.Max_salinity,
                                       Min_water_retention = s.Min_water_retention,
                                       Max_water_retention = s.Max_water_retention,
                                       Plants = s.Plants.Select(p => p.Plant_id).ToList(),
                                   }).FirstOrDefault<SoilModel>();
            }
            if (Soil == null) return NotFound();
            return Ok(Soil);
        }
    }
}
