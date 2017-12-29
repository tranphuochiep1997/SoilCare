using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoilCare.WebAPI.Models
{
    public class AddPlantModel
    {
        
        public string Plant_name { get; set; }
        public string Plant_image { get; set; }
        public string Plant_discription { get; set; }
    }
    public class PlantModel : AddPlantModel
    {
        public string Plant_id { get; set; }
        public string Soil_id { get; set; }
        public SoilModel Soil { get; set; }
    }
}