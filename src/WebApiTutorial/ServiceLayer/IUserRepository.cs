namespace WebApiTutorial.ServiceLayer
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// A simple user repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The new User.</returns>
        User Create(User user);

        /// <summary>
        /// Get all available users.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> Get();

        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetById(int id);
    }
}