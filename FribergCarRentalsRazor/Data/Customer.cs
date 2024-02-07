using System.ComponentModel.DataAnnotations;

namespace FribergCarRentalsRazor.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }
    }
}
