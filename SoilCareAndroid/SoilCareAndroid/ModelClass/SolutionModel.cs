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
    class SolutionModel
    {
        public string Solution_name { get; set; }
        public string Quantity { get; set; }
        public Nullable<double> Value { get; set; }
        public string Solution_description { get; set; }
        public string Unit_symbol { get; set; }
    }
}