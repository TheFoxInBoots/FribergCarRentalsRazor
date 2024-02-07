using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FribergCarRentalsRazor.Data;

namespace FribergCarRentalsRazor.Data
{
    public class FribergCarRentalsRazorContext : DbContext
    {
        public FribergCarRentalsRazorContext (DbContextOptions<FribergCarRentalsRazorContext> options)
            : base(options)
        {
        }

        public DbSet<FribergCarRentalsRazor.Data.Car> Car { get; set; } = default!;

        public DbSet<FribergCarRentalsRazor.Data.Customer> Customer { get; set; } = default!;

        public DbSet<FribergCarRentalsRazor.Data.Booking> Booking { get; set; } = default!;

        public DbSet<FribergCarRentalsRazor.Data.AdminUser> Admin { get; set; } = default!;           
    }
}
