using System.ComponentModel.DataAnnotations;

namespace FribergCarRentalsRazor.Data
{
    public class AdminUser
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }
    }
}
