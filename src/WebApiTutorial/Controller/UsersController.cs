namespace WebApiTutorial.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Models;
    using ServiceLayer;

    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            this.userRepository = userRepository;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return this.userRepository.Get();
        }

        [Route("{id}")]
        [HttpGet]
        public User Get(int id)
        {
            var user = this.userRepository.GetById(id);
            if (user == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            return user;
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(User user)
        {
            var newUser = this.userRepository.Create(user);

            return this.Created(this.Request.RequestUri + newUser.Id.ToString(), user);
        }
    }
}