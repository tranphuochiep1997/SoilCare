using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoilCareWebAPI.Models
{
    public class AddSolutionOfferModel
    {
        public string Measure_id { get; set; }
        public List<string> Solution_id { get; set; }
    }
    public class DeleteSoltuionOfferModel
    {
        public string Measure_id { get; set; }
        public string Solution_id { get; set; }
    }
    public class SolutionOfferModel
    {
        public string Solution_id { get; set; }
        public Nullable<double> Value_config { get; set; }
        public string Unit_symbol_config { get; set; }
        public string Unit_name_config { get; set; }
        public string Quantity_config { get; set; }
        public string Status { get; set; }
    }
}