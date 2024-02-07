using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;

namespace FribergCarRentalsRazor.Pages.Admin
{
    public class CarEditModel : PageModel
    {
        private readonly ICar carRep;

        public CarEditModel(ICar carRep)
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

        public async Task<IActionResult> OnPostAsync(Car car)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await carRep.EditCar(car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(Car.CarId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool CarExists(int id)
        {
            // Kontrollera om kunden med det angivna ID:t finns i databasen
            return carRep.GetCarById(id) != null;
        }
    }
}
