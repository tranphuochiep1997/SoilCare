using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace SoilCare.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            jsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
