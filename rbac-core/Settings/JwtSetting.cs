namespace rbac_core.Settings
{
    public sealed class JwtSettings
    {
        public required string SecretKey { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public int ExpiryHours { get; set; } = 24;
    }
}
