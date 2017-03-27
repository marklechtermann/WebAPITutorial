namespace WebApiTutorial.ServiceLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Infrastruture;
    using Models;
    using Newtonsoft.Json;

    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The in-memory user list.
        /// </summary>
        private List<User> users = new List<User>();

        public UserRepository(ILogger logger)
        {

            // Get some names from randomuser.me
            using (var client = new HttpClient())
            {
                var result = client.GetAsync("https://randomuser.me/api/?results=10&inc=name&nat=de").Result;
                var content = result.Content.ReadAsStringAsync().Result;

                var data = new
                {
                    results = new[]
                    {
                        new
                        {
                            name = new {first = "", last = ""}
                        }
                    }
                };

                var anonymousType = JsonConvert.DeserializeAnonymousType(content, data);

                int count = 1;
                foreach (var res in anonymousType.results)
                {
                    this.users.Add(new User {Id = count, Name = res.name.first, Surname = res.name.last});
                    count++;
                }
            }
        }

        /// <summary>
        /// Get all available users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Get()
        {
            return this.users;
        }

        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            return this.users.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The new User.</returns>
        public User Create(User user)
        {
            this.users.Add(user);
            return user;
        }
    }
}