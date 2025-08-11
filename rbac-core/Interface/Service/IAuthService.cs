using rbac_core.Entity;
using rbac_core.Enum;

namespace rbac_core.Interface.Service
{
    public interface IAuthService
    {
        string GenerateHash(string plainPassword);

        bool VerifyPassword(string plainPassword, string hashedPassword);

        string GenerateToken(User user);

        bool HasRole(Roles role);

        int GetUserID();

        HashSet<Roles> GetRoles();
    }
}
