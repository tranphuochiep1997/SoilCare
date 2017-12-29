using SoilCare.WebAPI.Data;
using SoilCare.WebAPI.Models;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SoilCare.WebAPI.Controllers
{
    public class SolutionsController : ApiController
    {
        // Get: api/Solutions
        public IHttpActionResult GetAllSolutions()
        {
            IList<SolutionModel> listSolution = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                listSolution = db.Solutions.Where(s => s.Owner.ToLower().Equals("system"))
                                           .Select(AutoMapper.Mapper.Map<Solution, SolutionModel>)
                                           .ToList();
            }
            if (listSolution.Count == 0) return NotFound();
            return Ok(listSolution);
        }
        // Get: api/Solutions/solutionid
        public IHttpActionResult GetSolutionById(string id)
        {
            SolutionModel _solution = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                Solution solution = db.Solutions.Find(id);
                if (solution == null)
                {
                    db.Dispose();
                    return NotFound();
                }
                _solution = Mapper.Map<Solution, SolutionModel>(solution);
            }
            return Ok(_solution);
        }

        // POST: api/Solutions/
        [ResponseType(typeof(Solution))]
        public IHttpActionResult PostSolution([FromBody]AddSolutionModel solution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Solution _solution;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                string initSolutionId = Guid.NewGuid().ToString("N");
                _solution = new Solution();
                _solution = Mapper.Map<AddSolutionModel, Solution>(solution);
                _solution.Solution_id = initSolutionId;
                _solution.Owner = "User";
                db.Solutions.Add(_solution);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (SolutionExists(db, _solution.Solution_id))
                    {
                        return Conflict();
                    }
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = _solution.Solution_id }, _solution);
        }
        private bool SolutionExists(SoilCareEntities db, string id)
        {
            return db.Solutions.Count(e => e.Solution_id.Equals(id)) > 0;
        }
    }
}
