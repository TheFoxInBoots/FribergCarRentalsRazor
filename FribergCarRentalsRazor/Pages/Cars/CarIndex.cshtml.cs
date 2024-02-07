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
    public class CarIndexModel : PageModel
    {
        private readonly ICar carRep;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CarIndexModel(ICar carRep, IHttpContextAccessor httpContextAccessor)
        {
            this.carRep = carRep;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<List<Car>> Car { get; set; } = default!;     

        public async Task OnGetAsync()
        {
            Car = carRep.GetAllCars();
        }
    }
}
