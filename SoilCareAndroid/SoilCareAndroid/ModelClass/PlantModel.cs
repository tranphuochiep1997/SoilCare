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
    public class AddPlantModel
    {
        public string Plant_name { get; set; }
        public string Plant_description { get; set; }
        public string Plant_image { get; set; }       
    }
    public class PlantModel : AddPlantModel
    {
        public string Plant_id { get; set; }
        public string Soil_id { get; set; }
        public SoilModel Soil { get; set; }
    }
}