using System.Threading;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using DrinkStore.WebApi.DependencyInjection;
using DrinkStore.WebApi.Owin;
using log4net;
using log4net.Config;
using Microsoft.Owin;
using Microsoft.Owin.BuilderProperties;
using Owin;
using WebApi.StructureMap;

[assembly: OwinStartup(typeof(OwinStartup))]

namespace DrinkStore.WebApi.Owin
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            Log4NetConfiguration();
            WebApiConfiguration(app);
        }

        private static void WebApiConfiguration(IAppBuilder app)
        {
            var token = new AppProperties(app.Properties).OnAppDisposing;
            if (token != CancellationToken.None)
                token.Register(ApplicationOnShutdown);

            var config = new HttpConfiguration();

            config.UseStructureMap(
                x => { x.AddRegistry<InMemoryRegistry>(); });

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            config.Services.Replace(typeof(IExceptionHandler), new ApplicationExceptionHandler());

            app.UseWebApi(config);
        }

        private static void ApplicationOnShutdown()
        {
            var log = LogManager.GetLogger(nameof(OwinStartup));
            log.Info($"Application shut down");
        }

        private static void Log4NetConfiguration()
        {
            XmlConfigurator.Configure();

            var log = LogManager.GetLogger(nameof(OwinStartup));
            log.Info($"Application start");
        }
    }
}