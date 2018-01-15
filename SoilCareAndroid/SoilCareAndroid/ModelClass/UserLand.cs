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
    public class UserLand
    {
        private string userLandName;
        private string userLandDescription;
        private int userLandImage;

        public UserLand(string userLandName, string userLandDescription, int userLandImage)
        {
            this.userLandName = userLandName;
            this.userLandDescription = userLandDescription;
            this.userLandImage = userLandImage;
        }
        public UserLand()
        {

        }
        public string UserLandName { get => userLandName; set => userLandName = value; }
        public string UserLandDescription { get => userLandDescription; set => userLandDescription = value; }
        public int UserLandImage { get => userLandImage; set => userLandImage = value; }
    }
}