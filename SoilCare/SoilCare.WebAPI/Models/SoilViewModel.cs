
using System.Collections.Generic;

namespace SoiCare.API.Models
{
    public class SoilViewModel
    {
        public string Soil_id { get; set; }
        public string Soil_name { get; set; }
        public double Min_nutrient { get; set; }
        public double Max_nutrient { get; set; }
        public double Min_humidity { get; set; }
        public double Max_humidity { get; set; }
        public double Min_acidity { get; set; }
        public double Max_acidity { get; set; }
        public double Min_porosity { get; set; }
        public double Max_porosity { get; set; }
        public double Min_water_retention { get; set; }
        public double Max_water_retention { get; set; }
        public double Min_salinity { get; set; }
        public double Max_salinity { get; set; }

        public ICollection<PlantViewModel> Plants { get; set; }
    }
}