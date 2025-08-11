using rbac_core.Interface.Service;
using static BCrypt.Net.BCrypt;

namespace rbac_core.Service
{
    public sealed class AuthService : IAuthService
    {
        public string GenerateHash(string plainPassword)
        {
            return HashPassword(plainPassword);
        }

        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            return Verify(plainPassword, hashedPassword);
        }
    }
}
