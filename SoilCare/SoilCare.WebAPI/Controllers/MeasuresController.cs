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
    public class MeasuresController : ApiController
    {
        // Get: api/Measures/id
        public IHttpActionResult GetMeasureById(string id)
        {
            MeasureDetail measure = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                measure = db.Measurements.Include("SolutionOffers").Include("Solutions")
                                         .Where(s => s.Measure_id.Equals(id))
                                         .Select(s => new MeasureDetail
                                         {
                                             Measure_id = s.Measure_id,
                                             Created_at = s.Created_at,
                                             Plant_name = s.Plant.Plant_name,
                                             Nutrient = s.Nutrient,
                                             Humidity = s.Humidity,
                                             Porosity = s.Porosity,
                                             Acidity = s.Acidity,
                                             Water_retention = s.Water_retention,
                                             Salinity = s.Salinity,
                                             Rate = s.Rate,
                                             HasSolution = s.SolutionOffers.Count > 0,
                                             Solution = s.SolutionOffers
                                                .Where(p => !p.Status.Equals("Deleted"))
                                                .Select(p => new SolutionWithStatusModel
                                                {
                                                    Solution_id = p.Solution_id,
                                                    Solution_name = p.Solution.Solution_name,
                                                    // if user configed Values and Units
                                                    // query from SolutionOffer table
                                                    // otherwise query from Solution table
                                                    Value = p.Value_config ?? p.Solution.Value,
                                                    Unit_symbol = p.Unit_symbol_config ?? p.Solution.Unit_symbol,
                                                    Unit_name = p.Unit_name_config ?? p.Solution.Unit_name,
                                                    Quantity = p.Quantity_config ?? p.Solution.Quantity,
                                                    Solution_purpose = p.Solution.Solution_purpose,
                                                    Solution_discription = p.Solution.Solution_discription,
                                                    Owner = p.Solution.Owner,
                                                    Status = p.Status,
                                                }).ToList(),
                                         }).FirstOrDefault();

            }
            if (measure == null) return NotFound();
            return Ok(measure);
        }
        // Get: api/Measures/measureid/solutions
        [Route("api/Measures/{measureid}/solutions")]
        public IHttpActionResult GetSolutionsByMeasureId(string measureid)
        {
            IList<SolutionWithStatusModel> listSolution = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                listSolution = db.SolutionOffers.Where(s => s.Measure_id.Equals(measureid))
                                             .Select(s => new SolutionWithStatusModel
                                             {
                                                 Solution_id = s.Solution_id,
                                                 Solution_name = s.Solution.Solution_name,
                                                 // if user configed Values and Units
                                                 // query from SolutionOffer table
                                                 // otherwise query from Solution table
                                                 Value = s.Value_config ?? s.Solution.Value,
                                                 Unit_symbol = s.Unit_symbol_config ?? s.Solution.Unit_symbol,
                                                 Unit_name = s.Unit_name_config ?? s.Solution.Unit_name,
                                                 Quantity = s.Quantity_config ?? s.Solution.Quantity,
                                                 Solution_purpose = s.Solution.Solution_purpose,
                                                 Solution_discription = s.Solution.Solution_discription,
                                                 Owner = s.Solution.Owner,
                                                 Status = s.Status,
                                             }).ToList();
            }
            if (listSolution.Count == 0) return NotFound();
            return Ok(listSolution);
        }
        // Post: api/Measures
        [ResponseType(typeof(Measurement))]
        public IHttpActionResult PostPlant(AddMeasureModel measure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string initId = Guid.NewGuid().ToString("N");
            Measurement _measure = new Measurement
            {
                Measure_id = initId,
                Created_at = measure.Created_at,
                Plant_id = measure.Plant_id,
                Land_id = measure.Land_id,
                Nutrient = measure.Nutrient,
                Humidity = measure.Humidity,
                Porosity = measure.Porosity,
                Acidity = measure.Acidity,
                Water_retention = measure.Water_retention,
                Salinity = measure.Salinity,
                Rate = null,
            };
            using (SoilCareEntities db = new SoilCareEntities())
            {
                db.Measurements.Add(_measure);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (MeasureExists(db, _measure.Measure_id))
                    {
                        return Conflict();
                    }
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = _measure.Measure_id }, _measure);
        }
        private bool MeasureExists(SoilCareEntities db, string id)
        {
            return db.Measurements.Count(e => e.Measure_id.Equals(id)) > 0;
        }
    }
}
