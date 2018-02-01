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
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid.AdapterClass
{
    class EditSolutionAdapter : BaseAdapter<SolutionModel>
    {
        List<SolutionModel> items;
        Activity context;

        public EditSolutionAdapter(Activity context, List<SolutionModel> items) : base()
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
                view = context.LayoutInflater.Inflate(Resource.Layout.EditSolutionView, null);
            view.FindViewById<TextView>(Resource.Id.solutionName).Text = item.Solution_name;
            view.FindViewById<EditText>(Resource.Id.value).Text = item.Value.ToString();
            view.FindViewById<EditText>(Resource.Id.quantity).Text = item.Quantity;
            view.FindViewById<EditText>(Resource.Id.unitSymbol).Text = item.Unit_symbol;
            view.FindViewById<EditText>(Resource.Id.description).Text = item.Solution_description;
            return view;
        }
    }
}