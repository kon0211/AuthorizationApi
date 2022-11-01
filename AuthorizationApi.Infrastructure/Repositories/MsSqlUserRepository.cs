using AuthorizationApi.Domain;
using AuthorizationApi.Domain.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthorizationApi.Infrastructure
{
    public class MsSqlUserRepository : IUserRepository
    {
        #region Members

        private readonly UserDbContext context;

        private readonly ILogger<MsSqlUserRepository> logger;

        #endregion //members

        #region Properties

        #endregion //properties

        #region Constructor 

        public MsSqlUserRepository(UserDbContext userDbContext, ILogger<MsSqlUserRepository> logger)
        {
            this.context = userDbContext;
            this.logger = logger;
        }

        #endregion //constructor

        #region Methods

        /// <summary>
        /// Create new account
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="passHash">User password hash</param>
        /// <param name="email">User email</param>
        /// <param name="name">User name</param>
        /// <returns>Id of created user</returns>
        /// <exception cref="AuthException"></exception>
        public async Task<int> CreateUser(string login, string passHash, string name, string email)
        {
            var user = new User()
            {
                Login = login,
                PasswordHash = passHash,
                Name = name,
                Email = email,
            };
            try
            {
                await context.Users.AddAsync(user);
                context.SaveChanges();
                return user.Id;
            }
            catch(DbUpdateException due)
            {
                if(due.InnerException is SqlException se && se.Number == 2601) // insert duplication key
                {
                    throw new AuthException("This login is already taken");
                }
                throw;
            }
        }

        /// <summary>
        /// Return 
        /// </summary>
        /// <param name="login">User's login</param>
        /// <returns>user, if user exists in database or null otherwise</returns>
        public async Task<User> GetUserByLogin(string login)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Login == login);
            return user;
        }

        /// <summary>
        /// Update user profile
        /// </summary>
        public async Task UpdateUser(User user)
        {
            context.Update(user);
            await context.SaveChangesAsync();
        }

        #endregion //methods

    }
}