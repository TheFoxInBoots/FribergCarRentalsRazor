using System.ComponentModel.DataAnnotations;

namespace FribergCarRentalsRazor.Data
{
    public class Car
    {
        public int CarId { get; set; }
        [Display(Name = "Märke")]
        public string Brand { get; set; }
        [Display(Name = "Modell")]
        public string Model { get; set; }
        [Display(Name = "Årsmodell")]
        public string Year { get; set; }
        [Display(Name = "Färg")]
        public string Color { get; set; }
        [Display(Name = "Bild")]
        public string Pic {  get; set; }
    }
}
