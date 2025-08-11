using Microsoft.AspNetCore.Mvc;
using rbac_core.Interface.Service;
using rbac_core.Views;

namespace rbac_web.Controller
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("list")]
        public PaginatedResponse<UserResponse> ListUsers(
            [FromBody] PaginatedRequest<UserListRequest> req
        )
        {
            return _userService.ListUsers(req);
        }

        [HttpPost]
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
        public async Task DeleteUser([FromQuery] int id)
        {
            await _userService.DeleteUser(id);
        }

        [HttpPatch]
        public async Task UpdatePassword([FromBody] ChangePasswordRequest req)
        {
            await _userService.UpdatePassword(req);
        }
    }
}
