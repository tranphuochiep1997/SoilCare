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
using SoilCare.Android.ModelClass;

namespace SoilCare.Android.AdapterClass
{
    public class UserLandAdapter : BaseAdapter<UserLand>
    {
        List<UserLand> items;
        Activity context;

        public UserLandAdapter(Activity context, List<UserLand> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override UserLand this[int position] => items[position];

        public override int Count => items.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public class ViewHolder: Java.Lang.Object
        {
            public TextView LandName;
            public TextView LandDescription;
            
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder viewHolder;
            if(convertView == null)
            {
                convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.user_land_row_view, parent,false);
                viewHolder = new ViewHolder();
                viewHolder.LandName = convertView.FindViewById<TextView>(Resource.Id.textViewLandName);
                viewHolder.LandDescription = convertView.FindViewById<TextView>(Resource.Id.textViewDescription);
                
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }

            var item = items[position];
            viewHolder.LandName.Text = item.UserLandName;
            viewHolder.LandDescription.Text = item.UserLandDescription;
            
            return convertView;
        }
    }
}