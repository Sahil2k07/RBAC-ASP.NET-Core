using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using rbac_core.Enum;
using rbac_core.Interface.Service;
using rbac_core.Settings;
using rbac_core.Views;

namespace rbac_web.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/v1/user")]
    public class UserController(IUserService userService, IOptions<JwtSettings> options)
        : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly JwtSettings _jwtSettings = options.Value;

        [HttpPost("list")]
        [Authorize(Roles = nameof(Roles.ADMIN))]
        public PaginatedResponse<UserResponse> ListUsers(
            [FromBody] PaginatedRequest<UserListRequest> req
        )
        {
            return _userService.ListUsers(req);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task Signup([FromBody] SignupRequest req)
        {
            await _userService.AddUser(req);
        }

        [HttpPut]
        public async Task UpdateUser([FromBody] UpdateUserRequest req)
        {
            await _userService.UpdateUser(req);
        }

        [HttpDelete]
        [Authorize(Roles = nameof(Roles.ADMIN))]
        public async Task DeleteUser([FromQuery] int id)
        {
            await _userService.DeleteUser(id);
        }

        [HttpPatch]
        public async Task UpdatePassword([FromBody] ChangePasswordRequest req)
        {
            await _userService.UpdatePassword(req);
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Signin([FromBody] SigninRequest req)
        {
            string token = await _userService.Signin(req);

            Response.Cookies.Append(
                _jwtSettings.TokenName,
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(_jwtSettings.ExpiryHours),
                }
            );

            return Ok(new { message = "Logged in successfully" });
        }
    }
}
