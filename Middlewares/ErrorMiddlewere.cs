
namespace ASP_Net_Core_MVC_Liberary.Middlewares
{
    public class ErrorMiddlewere
    {
        private readonly RequestDelegate _next;

        // Constructor to initialize the middleware with the next delegate in the pipeline
        public ErrorMiddlewere(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"[ErrorMiddlewere] {context.Request.Path}"); // Log the request path for debugging purposes
            await _next(context); // Call the next middleware in the pipeline
            Console.WriteLine($"[ErrorMiddleWere] {context.Response.StatusCode} "); // Log the response status code for debugging purposes

            if  (context.Response.StatusCode == 404)
            {
                context.Items["Message"] = "Gå hem";// Set a custom message in HttpContext.Items to be accessed in the Error action of HomeController
                context.Request.Path = "/Home/Error"; // Redirect to the Error action in HomeController
                Console.WriteLine("[ErrorMiddlewere] 404"); // Log the 404 error for debugging purposes

                await _next(context);
            }
        }
    }
}
