using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DrinkStore.WebApi.Owin
{
    public class ServerErrorResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }
        public string Message { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var messageJson = new
            {
                Message = Message,
            };

            var objectContent = new ObjectContent(messageJson.GetType(),
                messageJson,
                new JsonMediaTypeFormatter());

            return Task.FromResult(
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = objectContent,
                    RequestMessage = Request
                });
        }
    }
}