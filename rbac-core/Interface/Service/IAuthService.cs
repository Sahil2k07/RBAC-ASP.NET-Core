namespace rbac_core.Interface.Service
{
    public interface IAuthService
    {
        string GenerateHash(string plainPassword);

        bool VerifyPassword(string plainPassword, string hashedPassword);
    }
}
