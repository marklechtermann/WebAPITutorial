namespace WebApiTutorial.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Owin;

    /// <summary>
    /// A realy realy simple logging middelware.
    /// </summary>
    public class LoggingMiddleware : OwinMiddleware
    {
        public LoggingMiddleware(OwinMiddleware middleware) : base(middleware)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            Console.WriteLine($"{context.Request.Method} : {context.Request.Accept} : {context.Request.Path}");
            return this.Next.Invoke(context);
        }
    }
}