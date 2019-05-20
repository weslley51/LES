using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LES
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Serviços e configuração da API da Web
			// Rotas da API da Web
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
			config.Routes.MapHttpRoute("FilterApi", "api/{controller}/{action}");

			var formatters = GlobalConfiguration.Configuration.Formatters;
			config.Formatters.Remove(formatters.XmlFormatter);

			config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
			config.Formatters.JsonFormatter.SerializerSettings.Culture = System.Globalization.CultureInfo.CurrentCulture;
			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
		}
    }
}
