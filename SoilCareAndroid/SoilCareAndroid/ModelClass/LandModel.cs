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
    class LandModel
    {
        public string Land_id { get; set; }
        public string Land_name { get; set; }
        public string Land_address { get; set; }
        public string Land_image { get; set; }
        public Nullable<DateTime> Created_at { get; set; }
    }
}