using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using log4net;

namespace DrinkStore.WebApi.Owin
{
    public class ApplicationExceptionHandler : IExceptionHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            return !ShouldHandle(context)
                ? Task.FromResult(0)
                : HandleAsyncCore(context, cancellationToken);
        }

        public Task HandleAsyncCore(ExceptionHandlerContext context,
            CancellationToken cancellationToken)
        {
            HandleCore(context);
            return Task.FromResult(0);
        }

        public void HandleCore(ExceptionHandlerContext context)
        {
            LogException(context);

            context.Result = new ServerErrorResult
            {
                Message = "An internal error occured that could not be handled.  Please contact support for assistance.",
                Request = context.ExceptionContext.Request,
            };
        }

        private static void LogException(ExceptionHandlerContext context)
        {
            var logger = context.ExceptionContext.ControllerContext?.Controller?.ToString() ?? nameof(ExceptionHandler);

            var log = LogManager.GetLogger(logger);
            log.Error("An unhandled exception was encountered in the application.", context.Exception);
        }

        public bool ShouldHandle(ExceptionHandlerContext context)
        {
            return context.ExceptionContext.CatchBlock.IsTopLevel;
        }
    }
}