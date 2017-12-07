using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoiCare.API.Models
{
    public class PlantViewModel
    {
        public string Plant_id { get; set; }
        public string Plant_name { get; set; }
        public string Plant_image { get; set; }
        public string Plant_discription { get; set; }
        public string Soil_id { get; set; }

        public SoilViewModel Soil { get; set; }
    }
}