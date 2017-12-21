using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoilCare.WebAPI.Models
{
    public class LandModel
    {
        public string Land_id { get; set; }
        public string Land_name { get; set; }
        public string Land_address { get; set; }
        public string Land_image { get; set; }
        public Nullable<DateTime> Created_at { get; set; }

        public ICollection<MeasureModel> Measures { get; set; }
    }
    public class AddLandModel
    {
        public string Land_name { get; set; }
        public string Land_address { get; set; }
        public string Land_image { get; set; }
        public string User_id { get; set; }
    }
}