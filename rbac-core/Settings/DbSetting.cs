namespace rbac_core.Settings
{
    public sealed class DbSettings
    {
        public required string Host { get; set; }
        public required string Database { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }
    }
}
