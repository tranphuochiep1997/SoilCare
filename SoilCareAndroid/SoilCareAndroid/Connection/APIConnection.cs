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
        private RestClient client;
        public APIConnection()
        {
            client = new RestClient(apiUrl);
        }

        // List Request Type
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

        // Access Land Table
        public static string Lands = "Lands";

        // Return information of a land by land Id
        public static string LandById = "Lands/{id}";

        // Return measured history of a land by land Id
        public static string MeasuresByLandId = "Lands/{id}/Measures";

        //        QUERY FOR HOME PAGE
        // return list of user's land
        public static string UserLand = "Users/user001/Lands";


        // Get data without id
        public T GetData<T>(string requestType) where T : new()
        {
            RestRequest request = new RestRequest(requestType, Method.GET);

            var response = client.Execute<T>(request);
            return response.Data;
        }

        // Get data with an id
        public T GetData<T>(string requestType, string id) where T : new()
        {
            RestRequest request = new RestRequest(Method.GET);
            request.Resource = requestType;
            request.AddParameter("id", id, ParameterType.UrlSegment);

            var response = client.Execute<T>(request);
            return response.Data;
        }

        // Post method
        public bool PostData(string requestType, object model)
        {
            RestRequest request = new RestRequest(Method.POST);
            request.Resource = requestType;
            request.AddObject(model);
            var response = client.Execute(request);
            if (response.IsSuccessful) return true;
            return false;
        }

        // Put Data
        public bool PutData(string requestType, string id, object model)
        {
            RestRequest request = new RestRequest(Method.PUT);
            request.Resource = requestType;
            request.AddParameter("id", id, ParameterType.UrlSegment);
            request.AddObject(model);
            var response = client.Execute(request);
            if (response.IsSuccessful) return true;
            return false;
        }
    }
}