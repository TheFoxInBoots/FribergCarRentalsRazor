using System.ComponentModel.DataAnnotations;

namespace FribergCarRentalsRazor.Data
{
    public class Booking
    {
        [Display(Name = "BokningsId")]
        public int BookingId { get; set; }
        [Display(Name = "Kund")]
        public Customer Customer { get; set; }
        [Display(Name = "Bil")]
        public Car Car { get; set; }
        [Display(Name = "Kostnad")]
        public int Price { get; set; }
        public int CalculatedPrice
        {
            get
            {
                return ((EndDate - StartDate).Days + 1) * 150;
            }
        }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Slutdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public Booking()
        {
            StartDate = new DateTime(DateTime.Now.Year, 1, 1);
            EndDate = new DateTime(DateTime.Now.Year, 1, 1);

            //if (EndDate < StartDate) 
            //{
            //    EndDate = StartDate.AddDays(1); // Om EndDate är lagd tidigare än StartDate så sätts det till dagen efter StartDate
            
        }               
    }
}
