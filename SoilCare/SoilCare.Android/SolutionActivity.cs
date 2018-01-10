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
using SoilCare.Android.AdapterClass;
using SoilCare.Android.ModelClass;

namespace SoilCare.Android
{
    [Activity(Label = "SolutionActivity", MainLauncher = false, Theme = "@style/BasicTheme")]
    public class SolutionActivity : Activity
    {
        ListView listView;
        ImageButton backButton;
        ImageButton rateButton;
        ImageButton editButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
 
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Solution);
            
            List<SolutionModel> solutionList = new List<SolutionModel>()
            {
                new SolutionModel
                {
                    Solution_name = "Cuốc Bẫm",
                    Quantity = "Depth",
                    Value = 30,
                    Unit_symbol = "Cmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm",
                    Solution_description = "Dùng cuốc, cuốc liên tục xuống đất " +
                    "cho đến khi mệt thì nghỉ."
                },
                new SolutionModel
                {
                    Solution_name = "Bơm nước",
                    Quantity = "Volume",
                    Value = 1000,
                    Unit_symbol = "L",
                    Solution_description = "Có thể trổ nước từ kênh hoặc bơm nước."
                },
                new SolutionModel
                {
                    Solution_name = "Bón phân hữu cơ",
                    Quantity = "Amount",
                    Value = 5,
                    Unit_symbol = "Packages(s)",
                    Solution_description = "Bón phân hữu cơ đều khắp đất."
                }
            };
            backButton = FindViewById<ImageButton>(Resource.Id.button_back);
            rateButton = FindViewById<ImageButton>(Resource.Id.button_rate);
            editButton = FindViewById<ImageButton>(Resource.Id.button_edit);
            listView = FindViewById<ListView>(Resource.Id.list);
            listView.Adapter = new SolutionAdapter(this, solutionList);

            backButton.Click += delegate
            {
                this.OnBackPressed();
            };
            editButton.Click += delegate
            {
                Intent editSolutionActivity = new Intent(this, typeof(EditSolutionActivity));
                StartActivity(editSolutionActivity);
            };
        }
        
    }
}