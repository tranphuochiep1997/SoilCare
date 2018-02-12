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
using SoilCareAndroid.Fragments;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid.AdapterClass
{
    class LandAdapter : BaseAdapter<LandModel>
    {
        List<LandModel> items;
        Activity context;

        public LandAdapter(List<LandModel> items, Activity context): base()
        {
            this.items = items;
            this.context = context;
        }

        public override LandModel this[int position] => items[position];

        public override int Count => items.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public class ViewHolder : Java.Lang.Object
        {
            public TextView LandName;
            public TextView LandDescription;
            public ImageView ImageView;
            public ImageButton ImageButton;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder viewHolder;
            if (convertView == null)
            {
                convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.user_land_row_view, parent, false);
                viewHolder = new ViewHolder();
                viewHolder.ImageView = convertView.FindViewById<ImageView>(Resource.Id.imageViewUserLand);
                viewHolder.LandName = convertView.FindViewById<TextView>(Resource.Id.textViewLandName);
                viewHolder.LandDescription = convertView.FindViewById<TextView>(Resource.Id.textViewDescription);
                viewHolder.ImageButton = convertView.FindViewById<ImageButton>(Resource.Id.imageButtonEdit);

                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }

            var item = items[position];

            var bitmapImage = BitmapHelper.GetImageBitmapFromUrl(item.Land_image);
            viewHolder.ImageView.SetImageBitmap(bitmapImage);
            viewHolder.LandName.Text = item.Land_name;
            viewHolder.LandDescription.Text = item.Land_address;
            viewHolder.ImageButton.Visibility = ViewStates.Gone;
            //viewHolder.ImageButton.Click += delegate
            //{
            //    context.StartActivity(typeof(EditUserLandActivity));
            //};

            return convertView;
        }
        public void refreshEvents(List<LandModel> items)
        {
            this.items.Clear();
            this.items.AddRange(items);
            NotifyDataSetChanged();
        }
    }
}