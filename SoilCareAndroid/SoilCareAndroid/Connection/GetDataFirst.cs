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

namespace SoilCareAndroid.Connection
{
    public class GetDataFirst
    {              
        //List<AddLandModel> listAddLandModel;
        public List<LandModel> GetListLandModel(String userID)
        {
            List<LandModel> listLandModel = new List<LandModel>();
            APIConnection connector = new APIConnection();
            listLandModel = connector.GetData<List<LandModel>>(APIConnection.LandsByUserId, userID);
            return listLandModel;
        }

        public List<AddPlantModel> GetListAddPlantModel()
        {
            List<AddPlantModel> listAddPlantModel = new List<AddPlantModel>();
            APIConnection connector = new APIConnection();
            listAddPlantModel  = connector.GetData<List<AddPlantModel>>(APIConnection.Plants);
            return listAddPlantModel;
        }

        private static String GetUserID()
        {
            MainActivity userId = new MainActivity();
            return userId.getMyData();
        }
    }
}