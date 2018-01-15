using System;

namespace SoilCareWebAPI.Models
{
    public class SolutionModel
    {
        public string Solution_id { get; set; }
        public string Solution_name { get; set; }
        public Nullable<double> Value { get; set; }
        public string Unit_symbol { get; set; }
        public string Unit_name { get; set; }
        public string Quantity { get; set; }
        public string Solution_purpose { get; set; }
        public string Solution_description { get; set; }
        public string Owner { get; set; }
    }
    public class SolutionWithStatusModel : SolutionModel
    {
        public string Status { get; set; }
    }
    public class AddSolutionModel
    {
        public string Solution_name { get; set; }
        public Nullable<double> Value { get; set; }
        public string Unit_symbol { get; set; }
        public string Unit_name { get; set; }
        public string Quantity { get; set; }
        public string Solution_purpose { get; set; }
        public string Solution_description { get; set; }
    }
}