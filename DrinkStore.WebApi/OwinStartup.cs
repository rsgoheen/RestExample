using System.Web.Http;
using DrinkStore.WebApi;
using DrinkStore.WebApi.DependencyInjection;
using Microsoft.Owin;
using Owin;
using WebApi.StructureMap;

[assembly: OwinStartup(typeof(OwinStartup))]

namespace DrinkStore.WebApi
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.UseStructureMap(
            x =>
            {
                x.AddRegistry<InMemoryRegistry>();
            });

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}