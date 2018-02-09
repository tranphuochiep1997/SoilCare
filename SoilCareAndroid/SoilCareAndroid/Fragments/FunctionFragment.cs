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

namespace SoilCareAndroid.Fragments
{
    public class FunctionFragment : Android.Support.V4.App.Fragment
    {
        public static void ReplaceFragment(global::Android.Support.V4.App.Fragment fragment)
        {
            //fragment = new global::Android.Support.V4.App.Fragment();
            //global::Android.Support.V4.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
            //transaction.Replace(Resource.Id.root_frame, fragment);
            //transaction.SetTransition(global::Android.Support.V4.App.FragmentTransaction.TransitFragmentOpen);
            //transaction.AddToBackStack(null);
            //transaction.Commit();
        }
    }
}