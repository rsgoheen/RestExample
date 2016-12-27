using System.Web.Http;
using DrinkStore.WebApi;
using DrinkStore.WebApi.DependencyInjection;
using DrinkStore.WebApi.Repository;
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
            GlobalConfiguration.Configuration.UseStructureMap(
                x =>
                {
                    x.AddRegistry<InMemoryRegistry>();
                });
        }
    }
}