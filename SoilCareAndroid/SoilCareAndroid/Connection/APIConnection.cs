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
        public static string GetPlants = "Plants";
        public static string GetPlantById = "Plants/{id}";
        public static string GetSolutions = "Solutions";
        public static string GetUserById = "Users/{id}";
        public static string GetCodeByTelephone = "Users/telephone/{id}";
        public static string GetSoils = "Soils";
        public static string GetSoilById = "Soils/{id}";

        // List Request Type for Post method


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