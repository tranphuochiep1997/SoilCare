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
    class ImgurResponseModel
    {
        public class LinkAndDelete
        {
            public string id { get; set; }
            public string link { get; set; }
            public string deletehash { get; set; }
        }
        public LinkAndDelete data { get; set; }
        public int status { get; set; }
        public bool success { get; set; }
    }
    class AccessToken
    {
        public string access_token { get; set; }
    }
}