using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;

namespace FribergCarRentalsRazor.Pages.Cars
{
    public class CarDetailsModel : PageModel
    {
        private readonly ICar carRep;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CarDetailsModel(ICar carRep, IHttpContextAccessor httpContextAccessor)
        {
            this.carRep = carRep;
            this.httpContextAccessor = httpContextAccessor;
        }

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
    }
}
