using Android.Graphics;
using RestSharp;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid.Connection
{
    public class APIConnection
    {
        private const string apiUrl = "http://soilcarewebapi.azurewebsites.net/api";
        private const string imgurApi = "https://api.imgur.com/";

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
        public static string CodeByTelephone = "Users/GetCode/{id}";

        public static string UserByTelephone = "Users/telephone/{id}";


        //        QUERY FOR SOIL
        // Return all kinds of soils
        public static string Soils = "Soils";

        // Return a soil information by soil id
        public static string SoilById = "Soils/{id}";

        //        QUERY FOR LAND
        // Return a list of all lands of a user by his Id
        public static string LandsByUserId = "Users/{id}/Lands";
        // Post a new land 
        public static string AddNewLand = "Lands";

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

        // Post method, create new one
        public bool PostData(string requestType, object model)
        {
            RestRequest request = new RestRequest(Method.POST);
            request.Resource = requestType;
            request.AddObject(model);
            var response = client.Execute(request);
            return response.IsSuccessful;
        }

        // Return access link of image
        // UserOrLandImage can be "avatar" or "land" to access folder
        public string PostImage(Bitmap bitmap, string AvatarOrLandImage)
        {
            string albumHash;
            if (AvatarOrLandImage.ToLower().Equals("avatar"))
            {
                albumHash = "JqYSF";
            } else if (AvatarOrLandImage.ToLower().Equals("land"))
            {
                albumHash = "ff15H";
            } else
            {
                return "Invalid AvatarOrLandImage";
            }
            string imageBase64 = BitmapHelper.BitmapToBase64(bitmap);
            string accessToken = GenerateImgurAccessToken();

            RestClient client = new RestClient(imgurApi);
            RestRequest request = new RestRequest("3/image", Method.POST);

            request.AddHeader("Authorization", "Bearer "+accessToken);
            request.AddParameter("image", imageBase64);
            request.AddParameter("album", albumHash);
            request.AddParameter("type", "base64");

            var response = client.Execute<ImgurResponseModel>(request);
            if (response.Data.success)
            {
                return response.Data.data.link;
            }
            return "Error when upload image!";
        }
        public bool DeleteImage(string oldImageLink)
        {
            int lastSlash = oldImageLink.LastIndexOf('/') + 1;
            int lastDot = oldImageLink.LastIndexOf('.');
            string imageId = oldImageLink.Substring(lastSlash, lastDot - lastSlash);
            string accessToken = GenerateImgurAccessToken();

            // Use image Id to get deletehash first
            RestClient client = new RestClient(imgurApi);
            RestRequest request = new RestRequest("3/image/{imageHash}", Method.DELETE);
            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AddParameter("imageHash", imageId, ParameterType.UrlSegment);
            var response = client.Execute(request);

            return response.IsSuccessful;
        }

        // Put Data, update 
        public bool PutData(string requestType, string id, object model)
        {
            RestRequest request = new RestRequest(Method.PUT);
            request.Resource = requestType;
            request.AddParameter("id", id, ParameterType.UrlSegment);
            request.AddObject(model);
            var response = client.Execute(request);
            return response.IsSuccessful;
        }
        private string GenerateImgurAccessToken()
        {
            RestClient client = new RestClient(imgurApi);
            RestRequest request = new RestRequest("oauth2/token", Method.POST);
            request.AddParameter("refresh_token", "c6e298c62805b388ba1d0bfdc5752c42f3fad9d8", ParameterType.GetOrPost);
            request.AddParameter("client_id", "2c2c92711774e09", ParameterType.GetOrPost);
            request.AddParameter("client_secret", "9dd26dbaaf63d1d6e241e51969639254970a359c", ParameterType.GetOrPost);
            request.AddParameter("grant_type", "refresh_token", ParameterType.GetOrPost);
            var response = client.Execute<AccessToken>(request);
            return response.Data.access_token;
        }
    }
}