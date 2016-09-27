using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ElCarro.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<VehiclePart> VehiclePart { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<BugReport> BugReports { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}