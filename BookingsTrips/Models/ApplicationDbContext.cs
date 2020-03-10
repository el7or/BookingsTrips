using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BookingsTrips.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Boat> Boats { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<UserCabinsCount> UserCabinsCounts { get; set; }
    }
}