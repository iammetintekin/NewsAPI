using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRNews.Business.LoadServices;
using TRNews.Entity.DTOs.Identity;
using TRNews.Utilities.Attributes;

namespace TRNews.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto model)
        {
            var loginResult = await _serviceManager.Users.ValidateUser(model);
            if (!loginResult.Success)
            {
                _serviceManager.Logger.Error(loginResult.Message); 
                return BadRequest();// 401
            }
            return Ok(await _serviceManager.Users.CreateToken(true));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto Model)
        {
            var createResult = await _serviceManager.Users.RegisterUser(Model);
            if(!createResult.Success) 
            {
                _serviceManager.Logger.Error(createResult.Message);
                ModelState.TryAddModelError("err", createResult.Message);
                return BadRequest(ModelState);
            }
            return Ok(createResult);
        }

        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] TokenDto Model)
        {
            var tokenDtoResult = await _serviceManager.Users.RefreshToken(Model);
            return Ok(tokenDtoResult);
        }
    }
}
