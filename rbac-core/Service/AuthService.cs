using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using rbac_core.Entity;
using rbac_core.Enum;
using rbac_core.Interface.Service;
using rbac_core.Settings;
using static BCrypt.Net.BCrypt;

namespace rbac_core.Service
{
    public sealed class AuthService(IOptions<JwtSettings> options, IHttpContextAccessor httpContext)
        : IAuthService
    {
        private readonly JwtSettings _jwtSettings = options.Value;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public string GenerateHash(string plainPassword)
        {
            return HashPassword(plainPassword);
        }

        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            return Verify(plainPassword, hashedPassword);
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("id", user.ID.ToString()),
            };

            string[] roles = user.Roles.Split(",");

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiryHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool HasRole(Roles role)
        {
            return GetRoles().Contains(role);
        }

        public int GetUserID()
        {
            var userIdClaim = _httpContext.HttpContext?.User?.FindFirst("id")?.Value;
            if (int.TryParse(userIdClaim, out var id))
                return id;

            throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
        }

        public HashSet<Roles> GetRoles()
        {
            var roleClaims =
                _httpContext
                    .HttpContext?.User?.FindAll(ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList() ?? [];

            var roles = new HashSet<Roles>();
            foreach (var roleStr in roleClaims)
            {
                if (System.Enum.TryParse<Roles>(roleStr, out var role))
                    roles.Add(role);
            }

            return roles;
        }
    }
}
