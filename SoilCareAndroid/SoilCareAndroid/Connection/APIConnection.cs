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
using RestSharp;

namespace SoilCareAndroid.Connection
{
    class APIConnection
    {
        private const string apiUrl = "http://soilcarewebapi.azurewebsites.net/api";

        // List Request Type for Get method
        //=========================================================

        //        QUERY FOR PLANT
        // Return all plants in library
        public static string Plants = "Plants";

        //Return a plant and its Soil by plant Id
        public static string PlantById = "Plants/{id}";


        //        QUERY FOR SOLUTION
        // Return all solutions
        public static string Solutions = "Solutions";

        // Return a solution by solution Id
        public static string SolutionsById = "Solutions/{id}";

        // Return list of solutions of a measurement
        public static string SolutionsByMeasureId = "Measures/{id}/Solutions";

        //        QUERY FOR USER INFORMATION
        // Return user information by user Id
        public static string UserById = "Users/{id}";
        
        // Tạm thời sẽ trả về số 2018
        public static string CodeByTelephone = "Users/telephone/{id}";


        //        QUERY FOR SOIL
        // Return all kinds of soils
        public static string Soils = "Soils";

        // Return a soil information by soil id
        public static string SoilById = "Soils/{id}";

        //        QUERY FOR LAND
        // Return a list of all lands of a user by his Id
        public static string LandsByUserId = "Users/{id}/Lands";

        // Return information of a land by land Id
        public static string LandById = "Lands/{id}";

        // Return measured history of a land by land Id
        public static string MeasuresByLandId = "Lands/{id}/Measures";

        //        QUERY FOR HOME PAGE
        // return list of user's land
        public static string UserLand = "Users/user001/Lands";

        // List Request Type for Post method
        //=============================================================


        // Get data without id
        public T GetData<T>(string requestType) where T : new()
        {
            RestClient client = new RestClient(apiUrl);
            RestRequest request = new RestRequest(requestType, Method.GET);

            var response = client.Execute<T>(request);
            return response.Data;
        }

        // Get data with an id
        public T GetData<T>(string requestType, string id) where T : new()
        {
            RestClient client = new RestClient(apiUrl);
            RestRequest request = new RestRequest(Method.GET);
            request.Resource = requestType;
            request.AddParameter("id", id, ParameterType.UrlSegment);

            var response = client.Execute<T>(request);
            return response.Data;
        }

    }
}