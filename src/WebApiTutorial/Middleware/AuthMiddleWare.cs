namespace WebApiTutorial.Middleware
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Owin;

    public class AuthMiddleWare : OwinMiddleware
    {
        public AuthMiddleWare(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            //context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
            //return context.Response.WriteAsync("Forbidden!!!!");

            return this.Next.Invoke(context);
        }
    }
}