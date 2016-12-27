using System.Reflection;
using System.Web.Http;

namespace DrinkStore.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class AboutController : ApiController
    {
        [Route("")]
        [Route("about")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(
                new
                {
                    Name = "Shopping list (drinks) web API",
                    Version = $"{Assembly.GetExecutingAssembly().GetName().Version}"
                });
        }
    }
}