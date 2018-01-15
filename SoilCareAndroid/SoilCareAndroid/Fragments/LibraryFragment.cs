using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using SoilCareAndroid.AdapterClass;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid.Fragments
{
    public class LibraryFragment : global::Android.Support.V4.App.Fragment
    {
        private ListView listView;
        private TextView textViewLibrary;
        private EditText editTextSearchLibrary;
        private ImageButton imageButton;

        private List<PlantInfo> list;
        private LibraryAdapter adapter;             

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.LibraryFragment, container, false);
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();
            TestData();

            imageButton.Click += delegate
            {
                adapter.Filter.InvokeFilter(editTextSearchLibrary.Text);
               
                if (editTextSearchLibrary.Visibility == ViewStates.Visible)
                {
                    editTextSearchLibrary.Visibility = ViewStates.Gone;
                    editTextSearchLibrary.ClearFocus();
                    textViewLibrary.Visibility = ViewStates.Visible;
                    HideKeyboard(editTextSearchLibrary);
                }
                else
                {
                    editTextSearchLibrary.Visibility = ViewStates.Visible;
                    editTextSearchLibrary.RequestFocus();
                    textViewLibrary.Visibility = ViewStates.Gone;
                    ShowKeyboard(editTextSearchLibrary);
                }
            };

            editTextSearchLibrary.TextChanged += EditTextSearchLibrary_TextChanged;

            listView.ItemClick += ListView_ItemClick;

        }
        public void HideKeyboard(View pView)
        {
            InputMethodManager inputMethodManager = Activity.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.HideSoftInputFromWindow(pView.WindowToken, HideSoftInputFlags.None);
        }

        public void ShowKeyboard(View pView)
        {
            pView.RequestFocus();

            InputMethodManager inputMethodManager = Activity.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.ShowSoftInput(pView, ShowFlags.Forced);
            inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var plantDetail = new Intent(this.Activity, typeof(PlantDetailActivity));
            plantDetail.PutExtra("PlantDetailData", "Data from HomeActivity");
            StartActivity(plantDetail);
        }



        private void EditTextSearchLibrary_TextChanged(object sender, global::Android.Text.TextChangedEventArgs e)
        {
            var searchItem = editTextSearchLibrary.Text;
            adapter.Filter.InvokeFilter(editTextSearchLibrary.Text);
        }

        private void TestData()
        {
            list = new List<PlantInfo>();     
            
            // Item click events showed wrong results , fix later.
            list.Add(new PlantInfo("Cam", " TRung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Tao", " Lao ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Buoi", " Viet Nam ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Oi", " TRung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Xoai", " Thai Lan ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Man", " Nga ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Dao", " My ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Nho", " Quang Binh ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Quyt", " TRung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Le", " Viet Nam ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Mang Cau", " TRung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Mit uot", " Han Quoc ", Resource.Drawable.icon_soilcare2));
            
            adapter = new LibraryAdapter(this.Activity, list);
            listView.Adapter = adapter;
            listView.TextFilterEnabled = true;
        }
       
        private void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.listViewLibrary);
            textViewLibrary = this.View.FindViewById<TextView>(Resource.Id.textViewLibrary);
            editTextSearchLibrary = this.View.FindViewById<EditText>(Resource.Id.editTextSearchLibrary);
            imageButton = this.View.FindViewById<ImageButton>(Resource.Id.imageButtonSearchLibrary);          
        }

        private class SearchViewExpandListener
            : Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
        {
            private readonly IFilterable _adapter;

            public SearchViewExpandListener(IFilterable adapter)
            {
                _adapter = adapter;
            }

            public bool OnMenuItemActionCollapse(IMenuItem item)
            {
                _adapter.Filter.InvokeFilter("");
                return true;
            }

            public bool OnMenuItemActionExpand(IMenuItem item)
            {
                return true;
            }
        }
    }
    
}