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
    public class SolutionsController : ApiController
    {
        // Get: api/Solutions
        public IHttpActionResult GetAllSolutions()
        {
            IList<SolutionModel> listSolution = null;
            using (SoilCareEntities db = new SoilCareEntities())
            {
                listSolution = db.Solutions.Select(s => new SolutionModel
                {
                    Solution_id = s.Solution_id,
                    Solution_name = s.Solution_name,
                    Value = s.Value,
                    Unit_symbol = s.Unit_symbol,
                    Unit_name = s.Unit_name,
                    Quantity = s.Quantity,
                    Solution_purpose = s.Solution_purpose,
                    Solution_discription = s.Solution_discription,
                    Owner = s.Owner,
                }).ToList();
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
                Solution s = db.Solutions.Find(id);
                if (s == null)
                {
                    db.Dispose();
                    return NotFound();
                }
                _solution = new SolutionModel
                {
                    Solution_id = s.Solution_id,
                    Solution_name = s.Solution_name,
                    Value = s.Value,
                    Unit_symbol = s.Unit_symbol,
                    Unit_name = s.Unit_name,
                    Quantity = s.Quantity,
                    Solution_purpose = s.Solution_purpose,
                    Solution_discription = s.Solution_discription,
                    Owner = s.Owner,
                };
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
                _solution = new Solution
                {
                    Solution_id = initSolutionId,
                    Solution_name = solution.Solution_name,
                    Value = solution.Value,
                    Unit_symbol = solution.Unit_symbol,
                    Unit_name = solution.Unit_name,
                    Quantity = solution.Quantity,
                    Solution_purpose = solution.Solution_purpose,
                    Solution_discription = solution.Solution_discription,
                    Owner = "User",
                };
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
