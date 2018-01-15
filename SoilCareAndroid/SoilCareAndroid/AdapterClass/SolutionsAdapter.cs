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
using Java.Lang;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid.AdapterClass
{
    class SolutionsAdapter : BaseAdapter<SolutionModel>
    {
        List<SolutionModel> items;
        Activity context;

        public SolutionsAdapter(Activity context, List<SolutionModel> items) :base()
        {
            this.context = context;
            this.items = items;
        }
        public override SolutionModel this[int position] => items[position];

        public override int Count => items.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.SolutionView, null);
            view.FindViewById<TextView>(Resource.Id.solutionName).Text = item.Solution_name;
            view.FindViewById<TextView>(Resource.Id.value).Text = item.Value.ToString();
            view.FindViewById<TextView>(Resource.Id.quantity).Text = item.Quantity;
            view.FindViewById<TextView>(Resource.Id.unit).Text = item.Unit_symbol;
            view.FindViewById<TextView>(Resource.Id.description).Text = item.Solution_description;
            return view;
        }
    }
}