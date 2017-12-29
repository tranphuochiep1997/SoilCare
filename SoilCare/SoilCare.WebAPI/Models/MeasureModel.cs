using System;
using System.Collections.Generic;

namespace SoilCare.WebAPI.Models
{
    public class AddMeasureModel
    {
        public Nullable<System.DateTime> Created_at { get; set; }
        public string Land_id { get; set; }
        public string Plant_id { get; set; }
        public Nullable<double> Nutrient { get; set; }
        public Nullable<double> Humidity { get; set; }
        public Nullable<double> Acidity { get; set; }
        public Nullable<double> Porosity { get; set; }
        public Nullable<double> Water_retention { get; set; }
        public Nullable<double> Salinity { get; set; }
    }
    public class MeasureModel
    {
        public string Measure_id { get; set; }
        public string Plant_name { get; set; }
        public bool HasSolution => Solution.Count > 0;
        public Nullable<DateTime> Created_at { get; set; }
        public Nullable<int> Rate { get; set; }
        public Nullable<double> Nutrient { get; set; }
        public Nullable<double> Humidity { get; set; }
        public Nullable<double> Acidity { get; set; }
        public Nullable<double> Porosity { get; set; }
        public Nullable<double> Water_retention { get; set; }
        public Nullable<double> Salinity { get; set; }

        public ICollection<SolutionWithStatusModel> Solution;
    }
}