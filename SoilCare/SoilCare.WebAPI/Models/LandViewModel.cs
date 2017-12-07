using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoilCare.WebAPI.Models
{
    public class LandViewModel
    {
        public string Land_id { get; set; }
        public string Land_name { get; set; }
        public string Land_address { get; set; }
        public DateTime Created_at { get; set; }
        public string Status { get; set; }
    }
}