using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookingsTrips.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        //public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //public class ApplicationRole : IdentityRole
    //{
    //    [Display(Name = "القسم"), Required]
    //    public string Title { get; set; }
    //    public string Description { get; set; }
    //    public bool? IsActive { get; set; }
    //    public bool? IsDeleted { get; set; }
    //    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    //}

    //public class ApplicationUserRole : IdentityUserRole<string>
    //{
    //    public virtual ApplicationUser User { get; set; }
    //    public virtual ApplicationRole Role { get; set; }
    //}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}