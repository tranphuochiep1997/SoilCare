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
    public class MeasuresController : ApiController
    {
        // Get: api/Measures/id
        public IHttpActionResult GetMeasureById(string id)
        {
            MeasureModel measure = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                measure = db.Measures.Include("SolutionOffers")
                                         .Where(s => s.Measure_id.Equals(id))
                                         .Select(AutoMapper.Mapper.Map<Measure, MeasureModel>)
                                         .FirstOrDefault();

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
                listSolution = db.SolutionOffers.Include("Solution")
                                             .Where(s => s.Measure_id.Equals(measureid))
                                             .Select(AutoMapper.Mapper.Map<SolutionOffer, SolutionWithStatusModel>)
                                             .ToList();
            }
            if (listSolution.Count == 0) return NotFound();
            return Ok(listSolution);
        }
        // Post: api/Measures
        [ResponseType(typeof(Measure))]
        public IHttpActionResult PostPlant(AddMeasureModel measure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Measure _measure = Mapper.Map<AddMeasureModel, Measure>(measure);
            _measure.Measure_id = Guid.NewGuid().ToString("N");

            using (SoilCareEntities db = new SoilCareEntities())
            {
                db.Measures.Add(_measure);
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
            return db.Measures.Count(e => e.Measure_id.Equals(id)) > 0;
        }
    }
}