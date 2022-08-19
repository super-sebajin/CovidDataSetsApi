using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CovidDataSetsApi.DataAccessLayer
{
    [Table("User")]
    public class User
    {
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }

        [Column("Username")]
        [Required]
        public string Username { get; set; }

        [Column("Password")]
        [Required]
        public string Password { get; set; }

        [Column("IsActive")]
        [Required]
        public bool IsActive { get; set; }
    }
}
                          