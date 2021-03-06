﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoilCareWebAPI.Models
{
    public class SoilModel
    {
        public string Soil_id { get; set; }
        public string Soil_name { get; set; }
        public Nullable<double> Min_nutrient { get; set; }
        public Nullable<double> Max_nutrient { get; set; }
        public Nullable<double> Min_humidity { get; set; }
        public Nullable<double> Max_humidity { get; set; }
        public Nullable<double> Min_acidity { get; set; }
        public Nullable<double> Max_acidity { get; set; }
        public Nullable<double> Min_porosity { get; set; }
        public Nullable<double> Max_porosity { get; set; }
        public Nullable<double> Min_water_retention { get; set; }
        public Nullable<double> Max_water_retention { get; set; }
        public Nullable<double> Min_salinity { get; set; }
        public Nullable<double> Max_salinity { get; set; }
    }
    public class SoilModelDetail : SoilModel
    {
        public ICollection<string> Plants { get; set; }
    }
}