namespace WebApiTutorial
{
    using System.Web.Http;
    using System.Web.Http.Dependencies;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using Infrastruture;
    using Microsoft.OData.Edm;
    using Microsoft.Practices.Unity;
    using Middleware;
    using Models;
    using Owin;
    using ServiceLayer;
    using Swashbuckle.Application;
    using Swashbuckle.OData;
    using Unity.WebApi;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            // enable Attribute routing 
            configuration.MapHttpAttributeRoutes();

            // Who needs XML?
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);

            // Setup the Unity DI resolver
            configuration.DependencyResolver = this.ConfigureResolver();


            // OpenAPI metadata stuff ...
            configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "MyApi");

                    // A custom provider for OData
                    c.CustomProvider(defaultProvider => new ODataSwaggerProvider(defaultProvider, c, configuration));
                })
                .EnableSwaggerUi();

            // Some logging
            app.Use<LoggingMiddleware>();

            // This could be a auth middleware ... use AuthorizeAttribute
            app.Use<AuthMiddleWare>();

            // The WebApi magic!
            app.UseWebApi(configuration);

            // ... an finally the OData routes
            configuration.MapODataServiceRoute("odata", null, GetEdmModel());

            // Set the supported query options.
            configuration.Select().Filter().Count().Expand().OrderBy();

            // We're done
            configuration.EnsureInitialized();
        }

        /// <summary>
        /// Returns the EDM Model
        /// </summary>
        /// <returns></returns>
        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<User>("ODataUsers");
            return builder.GetEdmModel();
        }

        /// <summary>
        /// Configure the Unity resolver.
        /// </summary>
        /// <returns></returns>
        public IDependencyResolver ConfigureResolver()
        {
            var container = new UnityContainer();

            container.RegisterType<IUserRepository, UserRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILogger, Logger>(new ContainerControlledLifetimeManager());

            return new UnityDependencyResolver(container);
        }
    }
}