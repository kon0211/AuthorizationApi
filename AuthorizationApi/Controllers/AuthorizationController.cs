using AuthorizationApi.Domain;
using AuthorizationApi.Domain.Exceptions;
using AuthorizationApi.Extentions;
using AuthorizationApi.Infrastructure;
using AuthorizationApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace AuthorizationApi.Controllers
{
    public class AuthorizationController : ControllerBase
    {
        #region Members

        private readonly IUserRepository userRepository;
        private readonly PasswordHasher<User> passwordHasher;
        private readonly ILogger<AuthorizationController> logger;

        #endregion //members

        #region Constructor

        public AuthorizationController(IUserRepository userRepository,
                                       PasswordHasher<User> passwordHasher,
                                       ILogger<AuthorizationController> logger)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.logger = logger;
        }

        #endregion //ctor

        #region Action methods

        [HttpPost("auth/login")]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            User user;

            #region retrive user
            try
            {
                user = await userRepository.GetUserByLogin(model.Login);
            }
            catch (Exception exc)
            {
                logger.LogError(exc, $"Login error");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorModel(exc.Message));
            }
            #endregion

            #region check login and pass
            if (user == null)
            {
                return Unauthorized(new ErrorModel("Incorrect login or password"));
            }
            if (passwordHasher.VerifyHashedPassword(null, user.PasswordHash, model.Password) == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new ErrorModel("Incorrect login or password"));
            }
            #endregion 

            string jwt = CreateJwtToken(user);
            return Ok(new { accsess_token = jwt });
        }

        [HttpPost("auth/register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var err = ModelState.GetErrorModel("Incorrect input data");
                return BadRequest(err);
            }

            try
            {
                var passHash = passwordHasher.HashPassword(null, model.Password);
                var id = await userRepository.CreateUser(model.Login, passHash, model.Name, model.Email);
                return Ok();
            }
            catch (AuthException ae)
            {
                return BadRequest(new ErrorModel(ae.Message));
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Registration error");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorModel(e.Message));
            }
        }

        #endregion //action methods

        #region Jwt

        /// <summary>
        /// Create jwt-token with user claims
        /// </summary>
        private string CreateJwtToken(User user)
        {
            var now = DateTime.UtcNow;
            var expiresDt = now + JwtAuthOptions.LIFETIME_MINUTES;

            // TODO: add non-secret user params
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Login));
            //claims.Add(new Claim(ClaimTypes.Surname, user.Surname ?? ""));
            //claims.Add(new Claim(ClaimTypes.MobilePhone, user.Phone ?? ""));
            //claims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: JwtAuthOptions.ISSUER,
                audience: JwtAuthOptions.AUDIENCE,
                claims: claims,
                notBefore: null,
                expires: expiresDt,
                signingCredentials: new SigningCredentials(JwtAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }

        #endregion //jwt

    }
}
