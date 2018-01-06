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

namespace SoilCare.Android.ModelClass
{
    public class PlantInfo
    {
        private string plantName;
        private string plantDescription;
        private int plantImage;
       

        public PlantInfo(string plantName, string plantDescription, int plantImage)
        {
            this.PlantName = plantName;
            this.PlantDescription = plantDescription;
            this.PlantImage = plantImage;
        }
        public PlantInfo()
        {

        }

        public string PlantName { get => plantName; set => plantName = value; }
        public string PlantDescription { get => plantDescription; set => plantDescription = value; }
        public int PlantImage { get => plantImage; set => plantImage = value; }


    }
}