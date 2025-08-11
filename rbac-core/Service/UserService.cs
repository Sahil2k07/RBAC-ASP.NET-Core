using rbac_core.Entity;
using rbac_core.Errors;
using rbac_core.Interface.Repository;
using rbac_core.Interface.Service;
using rbac_core.Views;

namespace rbac_core.Service
{
    public sealed class UserService(IUserRepository userRepository, IAuthService authService)
        : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthService _authService = authService;

        public async Task AddUser(SignupRequest req)
        {
            bool exists = await _userRepository.ValidateEmail(req.Email);

            if (exists)
            {
                throw new AlreadyExistsException("Email already registered");
            }

            var hash = _authService.GenerateHash(req.Password);

            var user = new User
            {
                Email = req.Email,
                Password = hash,
                Roles = string.Join(",", req.Roles.Select(r => r.ToString())),
            };

            user = await _userRepository.AddUser(user);

            var profile = new Profile
            {
                UserID = user.ID,
                FirstName = req.FirstName,
                LastName = req.LastName,
                Address = req.Address,
                Phone = req.Phone,
            };

            await _userRepository.AddProfile(profile);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public PaginatedResponse<UserResponse> ListUsers(PaginatedRequest<UserListRequest> req)
        {
            (int count, List<UserResponse> users) = _userRepository.ListUsers(req);

            return new PaginatedResponse<UserResponse>(count, users);
        }

        public async Task UpdatePassword(ChangePasswordRequest req)
        {
            User? user =
                await _userRepository.GetUserFromID(req.ID)
                ?? throw new NotFoundException("User not found");

            if (_authService.VerifyPassword(req.OldPassword, user.Password))
            {
                throw new ValidationException("Wrong old password");
            }

            user.Password = _authService.GenerateHash(req.NewPassword);

            await _userRepository.UpdateUser(user);
        }

        public async Task UpdateUser(UpdateUserRequest req)
        {
            User? user =
                await _userRepository.GetUserFromID(req.ID)
                ?? throw new NotFoundException("User not found");

            user.Roles = string.Join(",", req.Roles.Select(r => r.ToString()));

            user.Profile = new Profile
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                UserID = user.ID,
                Address = req.Address,
                Phone = req.Phone,
            };

            await _userRepository.UpdateUser(user);
        }
    }
}
