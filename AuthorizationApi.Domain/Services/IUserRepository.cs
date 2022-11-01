
namespace AuthorizationApi.Domain
{
    public interface IUserRepository
    {
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="passHash">User password hash</param>
        /// <returns>Returns the id of created user</returns>
        public Task<int> CreateUser(string login, string passHash, string name, string email);

        /// <summary>
        /// Gets user by login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Return the user if it was found or null otherwise</returns>
        public Task<User> GetUserByLogin(string login);

        /// <summary>
        /// Update user info
        /// </summary>
        public Task UpdateUser(User user);

    }
}
