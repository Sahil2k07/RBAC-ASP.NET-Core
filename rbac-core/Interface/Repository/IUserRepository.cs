using rbac_core.Entity;
using rbac_core.Views;

namespace rbac_core.Interface.Repository
{
    public interface IUserRepository
    {
        Task<bool> ValidateEmail(string email);

        Task<User?> GetUser(string email);

        Task<User?> GetUserFromID(int id);

        Task DeleteUser(int id);

        Task UpdateUser(User user);

        (int, List<UserResponse>) ListUsers(PaginatedRequest<UserListRequest> req);

        Task<User> AddUser(User user);

        Task<Profile> AddProfile(Profile profile);
    }
}
