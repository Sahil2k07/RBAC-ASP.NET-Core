using System.ComponentModel.DataAnnotations;
using rbac_core.Enum;

namespace rbac_core.Views
{
    public sealed class SignupRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(
            100,
            MinimumLength = 6,
            ErrorMessage = "Password must be between 6 and 100 characters."
        )]
        public required string Password { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one role must be specified.")]
        public required HashSet<Roles> Roles { get; set; }
    }

    public sealed class UpdateUserRequest
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one role must be specified.")]
        public required HashSet<Roles> Roles { get; set; }
    }

    public sealed class ChangePasswordRequest
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(
            100,
            MinimumLength = 6,
            ErrorMessage = "Old password must be between 6 and 100 characters."
        )]
        public required string OldPassword { get; set; }

        [Required]
        [StringLength(
            100,
            MinimumLength = 6,
            ErrorMessage = "Old password must be between 6 and 100 characters."
        )]
        public required string NewPassword { get; set; }
    }

    public sealed class UserListRequest
    {
        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Roles? Role { get; set; }
    }

    public sealed class UserResponse
    {
        public required int ID { get; set; }
        public required string Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string[] Roles { get; set; } = [];
    }
}
