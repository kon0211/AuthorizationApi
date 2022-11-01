using AuthorizationApi.Domain;
using AuthorizationApi.Extentions;
using AuthorizationApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;

namespace AuthorizationApi.Controllers
{
    public class UserController : ControllerBase
    {
        #region Members

        private readonly IUserRepository userRepository;
        private readonly ILogger<UserController> logger;

        #endregion //members

        #region Constructor

        public UserController(IUserRepository userRepository,
                              ILogger<UserController> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        #endregion //constructor

        #region Action Methods

        [Authorize]
        [HttpGet("/account")]
        public async Task<IActionResult> UserInfo()
        {
            var user = await userRepository.GetUserByLogin(HttpContext.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            UserInfoModel userInfo = new UserInfoModel()
            {
                Login = user.Login,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Surname = user.Surname
            };

            string json = JsonConvert.SerializeObject(userInfo, Formatting.Indented);
            return Ok(json);
        }

        [Authorize]
        [HttpPut("/account/update")]
        public async Task<IActionResult> Update(UpdateUserModel model)
        {
            var user = await userRepository.GetUserByLogin(HttpContext.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                var err = ModelState.GetErrorModel("Incorrect input data");
                return BadRequest(err);
            }


            model.UpdateUser(user);

            try
            {
                await userRepository.UpdateUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, $"user update error");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorModel(e.Message));
            }
        }

        #endregion //action methods
    }
}
