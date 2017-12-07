using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoilCare.WebAPI.Models
{
    public class UserViewModel
    {
        public string User_id { get; set; }
        public string User_name { get; set; }
        public string Telephone { get; set; }
        public string Region { get; set; }
        public DateTime Created_at { get; set; }

        public ICollection<LandViewModel> Land { get; set; }
    }
}