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
using Android.Support.V4.App;

namespace SoilCareAndroid.AdapterClass
{
    public class ViewPagerAdapter : FragmentPagerAdapter
    {
        private List<global::Android.Support.V4.App.Fragment> list;

        public ViewPagerAdapter(global::Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
            list = new List<global::Android.Support.V4.App.Fragment>();
        }

        public override int Count => list.Count;

        public override global::Android.Support.V4.App.Fragment GetItem(int position)
        {
            return list[position];
        }

        public void AddFragment(global::Android.Support.V4.App.Fragment fragment)
        {
            list.Add(fragment);
        }
    }
}