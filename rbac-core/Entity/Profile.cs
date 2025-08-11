using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rbac_core.Entity
{
    [Table("PROFILE")]
    public sealed class Profile
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        [ForeignKey(nameof(User))]
        public required int UserID { get; set; }

        public User? User { get; set; }
    }
}
