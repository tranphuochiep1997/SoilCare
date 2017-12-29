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
    public class SoilsController : ApiController
    {
        // Get: api/Soils
        public IHttpActionResult GetAllSoils()
        {
            IList<SoilModel> listSoil = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                listSoil = db.Soils.Include("Plants")
                                .Select(AutoMapper.Mapper.Map<Soil, SoilModel>)
                                .ToList();
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
                                   .Select(AutoMapper.Mapper.Map<Soil, SoilModel>)
                                   .FirstOrDefault<SoilModel>();
            }
            if (Soil == null) return NotFound();
            return Ok(Soil);
        }
    }
}
