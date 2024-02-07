using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FribergCarRentalsRazor.Data;

namespace FribergCarRentalsRazor.Pages.Admin
{
    public class CarCreateModel : PageModel
    {
        private readonly ICar carRep;

        public CarCreateModel(ICar carRep)
        {
            this.carRep = carRep;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Car car)
        {
            if (!ModelState.IsValid || carRep == null || Car == null)
            {
                return Page();
            }

            await carRep.CreateCar(car);
            return RedirectToPage("./Index");
        }
    }
}
