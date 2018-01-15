using SoilCareWebAPI.Data;
using SoilCareWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SoilCareWebAPI.Controllers
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
                _offer = db.SolutionOffers.Where(s => s.Measure_id.Equals(measureId) && s.Solution_id.Equals(solutionId))
                                          .FirstOrDefault<SolutionOffer>();
                if (_offer == null)
                {
                    db.Dispose();
                    return NotFound();
                }
                _offer.Status = "Deleted";

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

                _offer = AutoMapper.Mapper.Map<SolutionOfferModel, SolutionOffer>(offer);
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
