using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoilCareWebAPI.Models
{
    public class AddUserModel
    {
        public string User_name { get; set; }
        public string User_image { get; set; }
        public string User_location { get; set; }
        public string User_email { get; set; }
        public string User_telephone { get; set; }

    }
    public class UserModel : AddUserModel
    {
        public string User_id { get; set; }
        public Nullable<System.DateTime> Created_at { get; set; }
    }
    public class UserModelDetail : UserModel
    {
        public IEnumerable<LandModel> Lands { get; set; }

    }
}