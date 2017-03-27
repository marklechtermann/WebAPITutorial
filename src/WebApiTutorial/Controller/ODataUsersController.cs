namespace WebApiTutorial.Controller
{
    using System.Linq;
    using System.Web.OData;
    using System.Web.OData.Routing;
    using Models;
    using ServiceLayer;
    using Infrastruture;

    public class ODataUsersController : ODataController
    {
        private readonly IUserRepository repo;

        public ODataUsersController(IUserRepository repo /*, ILogger logger*/)
        {
            // DI magic!
            this.repo = repo;
        }

        [EnableQuery(PageSize = 5)]
        [ODataRoute("ODataUsers")]
        public IQueryable<User> Get()
        {
            return this.repo.Get().AsQueryable();
        }
    }
}