using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SoilCareAndroid.ModelClass
{
    class UserInfo
    {
        public string User_telephone { get; set; }
        public string User_id { get; set; }
        public Nullable<DateTime> Created_at { get; set; }
        public string User_name { get; set; }
        public string User_location { get; set; }
        public string User_email { get; set; }
    }
}