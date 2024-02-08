using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;

namespace FribergCarRentalsRazor.Pages.Admin
{
    public class CarDeleteModel : PageModel
    {
        private readonly ICar carRep;

        public CarDeleteModel(ICar carRep)
        {
            this.carRep = carRep;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Car = await carRep.GetCarById(id.Value);
            if (Car == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Kontrollerar om CarId är null eller om carRep är null
            if (id == null || carRep == null)
            {
                TempData["Message2"] = "Bilen kunde inte hittas!";
                return Page(); 
            }

            // Hämtar kunden från CustomerRepository med det angivna CarId:t
            var car = await carRep.GetCarById(id.Value);
                        
            if (car != null)
            {
                Car = car;

                // Ta bort kunden från CustomerRepository
                carRep.DeleteCar(Car);

                TempData["Message"] = "Bilen har tagits bort!";
            }

            return RedirectToPage("/Cars/CarIndex"); 
        }
    }
}
