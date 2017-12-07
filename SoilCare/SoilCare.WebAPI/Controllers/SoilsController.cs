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
            List<SoilViewModel> listSoil = new List<SoilViewModel>();
            using (SoilCareEntities db = new SoilCareEntities())
            {
                listSoil = db.Soils.Select(s => new SoilViewModel
                {
                    Soil_id = s.Soil_id,
                    Soil_name = s.Soil_name,
                    Min_nutrient = (double)s.Min_nutrient,
                    Max_nutrient = (double)s.Max_nutrient,
                    Min_acidity = (double)s.Min_acidity,
                    Max_acidity = (double)s.Max_acidity,
                    Min_humidity = (double)s.Min_humidity,
                    Max_humidity = (double)s.Max_humidity,
                    Min_porosity = (double)s.Min_porosity,
                    Max_porosity = (double)s.Max_porosity,
                    Min_salinity = (double)s.Min_salinity,
                    Max_salinity = (double)s.Max_salinity,
                    Min_water_retention = (double)s.Min_water_retention,
                    Max_water_retention = (double)s.Max_water_retention,
                }).ToList<SoilViewModel>();
            }
            if (listSoil.Count == 0) return NotFound();
            return Ok(listSoil);
        }
        // Get: api/Soils/id
        public IHttpActionResult GetSoil(string id)
        {
            SoilViewModel listSoil = new SoilViewModel();
            using (SoilCareEntities db = new SoilCareEntities())
            {
                listSoil = db.Soils.Where(s => s.Soil_id.Equals(id))
                                   .Select(s => new SoilViewModel
                                   {
                                       Soil_id = s.Soil_id,
                                       Soil_name = s.Soil_name,
                                       Min_nutrient = (double)s.Min_nutrient,
                                       Max_nutrient = (double)s.Max_nutrient,
                                       Min_acidity = (double)s.Min_acidity,
                                       Max_acidity = (double)s.Max_acidity,
                                       Min_humidity = (double)s.Min_humidity,
                                       Max_humidity = (double)s.Max_humidity,
                                       Min_porosity = (double)s.Min_porosity,
                                       Max_porosity = (double)s.Max_porosity,
                                       Min_salinity = (double)s.Min_salinity,
                                       Max_salinity = (double)s.Max_salinity,
                                       Min_water_retention = (double)s.Min_water_retention,
                                       Max_water_retention = (double)s.Max_water_retention,
                                   }).FirstOrDefault<SoilViewModel>();
            }
            if (listSoil == null) return NotFound();
            return Ok(listSoil);
        }
    }
}
