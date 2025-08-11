using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rbac_core.Entity
{
    [Table("USER")]
    public sealed class User
    {
        [Key]
        public int ID { get; set; }

        [EmailAddress]
        [Required]
        public required string Email { get; set; }

        public required string Password { get; set; }

        public string Roles { get; set; } = "GUEST";

        public Profile? Profile { get; set; }
    }
}
