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

// Reference Link : https://blog.ostebaronen.dk/2014/03/filtering-listview-with-searchview-in.html
namespace SoilCareAndroid.AdapterClass
{
    public class LibraryAdapter : BaseAdapter<PlantInfo>, IFilterable
    {
        private List<PlantInfo> originalData;
        private List<PlantInfo> items;
        private readonly Activity context;

        public LibraryAdapter(Activity context, List<PlantInfo> items) : base()
        {
            this.context = context;
            this.items = items.OrderBy(s=> s.PlantName).ToList();

            Filter = new LibraryFilter(this);
        }

        public override PlantInfo this[int position] => items[position];

        public override int Count => items.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public class ViewHolder : Java.Lang.Object
        {
            public TextView plantName;
            public TextView plantDescription;
            public ImageView imageView;
            public ImageView tick;
            public TextView solution;

        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder viewHolder;
            if (convertView == null)
            {
                convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.library_row_view, parent, false);
                viewHolder = new ViewHolder();
                viewHolder.plantName = convertView.FindViewById<TextView>(Resource.Id.textViewPlantName);
                viewHolder.plantDescription = convertView.FindViewById<TextView>(Resource.Id.textViewPlantDescription);
                viewHolder.imageView = convertView.FindViewById<ImageView>(Resource.Id.imageViewLibrary);
                viewHolder.tick = convertView.FindViewById<ImageView>(Resource.Id.imgTick);
                viewHolder.solution = convertView.FindViewById<TextView>(Resource.Id.tvSolution);

            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }

            var item = items[position];
            viewHolder.plantName.Text = item.PlantName;
            viewHolder.plantDescription.Text = item.PlantDescription;
            viewHolder.imageView.SetImageResource(item.PlantImage);

            if (context.GetType().Equals(typeof(UserLandActivity))) {
                viewHolder.tick.Visibility = ViewStates.Visible;
                viewHolder.solution.Visibility = ViewStates.Visible;
            }
            
            
            convertView.Tag = viewHolder;

            return convertView;
        }

        // Implement Filter Interface
        public Filter Filter { get; private set; }
        public override void NotifyDataSetChanged()
        {
            // If you are using cool stuff like sections
            // remember to update the indices here!
            base.NotifyDataSetChanged();
        }

        private class LibraryFilter : Filter
        {
            private readonly LibraryAdapter _adapter;

            public LibraryFilter(LibraryAdapter adapter)
            {
                _adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<PlantInfo>();
                if (_adapter.originalData == null)
                    _adapter.originalData = _adapter.items;
                if (constraint == null) return returnObj;
                if(_adapter.originalData != null && _adapter.originalData.Any())
                {
                    //Compare constraint to all names lowercased
                    // if they are contained they are added to results.
                    results.AddRange(
                        _adapter.originalData.Where(s => s.PlantName.ToLower().Contains(constraint.ToString())));
                }
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter.items = values.ToArray<Java.Lang.Object>()
                        .Select(r => r.ToNetObject<PlantInfo>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }
        }
    }
}