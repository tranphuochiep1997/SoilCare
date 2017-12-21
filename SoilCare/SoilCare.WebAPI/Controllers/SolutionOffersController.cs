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
    public class SolutionOffersController : ApiController
    {
        // POST: api/SolutionOffers
        [ResponseType(typeof(AddSolutionOfferModel))]
        public IHttpActionResult PostSolutionsToMeasure(AddSolutionOfferModel offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string measureId = offer.Measure_id;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                foreach(string solutionId in offer.Solution_id)
                {
                    db.SolutionOffers.Add(new SolutionOffer {
                        Measure_id = measureId,
                        Solution_id = solutionId,
                        Value_config = null,
                        Unit_symbol_config = null,
                        Unit_name_config = null,
                        Quantity_config = null,
                        Status = "Unchanged",
                    });
                }
                
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = measureId }, offer);
        }
        // Delete: api/SolutionOffers
        [ResponseType(typeof(SolutionOffer))]
        public IHttpActionResult DeleteSolutionOffers(DeleteSoltuionOfferModel offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SolutionOffer _offer = null;
            string measureId = offer.Measure_id;
            string solutionId = offer.Solution_id;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                _offer = db.SolutionOffers.Where(s => s.Measure_id.Equals(measureId))
                                          .Where(s => s.Solution_id.Equals(solutionId))
                                          .FirstOrDefault<SolutionOffer>();
                if (_offer == null)
                {
                    db.Dispose();
                    return NotFound();
                }
                _offer.Status = "Deleted";
                //_offer.Measurement = null;
                //_offer.Solution = null;
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
        // PUT: api/SolutionOffers/measureid
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSolutionOffer(string id, SolutionOfferModel offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SolutionOffer _offer = null;
            using(SoilCareEntities db = new SoilCareEntities())
            {
                _offer = db.SolutionOffers.Find(new string[] { id, offer.Solution_id });

                if (_offer == null) return NotFound();

                // Map model to entity
                _offer.Solution_id = offer.Solution_id;
                _offer.Value_config = offer.Value_config;
                _offer.Unit_symbol_config = offer.Unit_symbol_config;
                _offer.Unit_name_config = offer.Unit_name_config;
                _offer.Quantity_config = offer.Quantity_config;
                _offer.Status = "Modified";

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
    }
}
