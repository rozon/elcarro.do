using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ElCarro.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>("DefaultConnection"));
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<VehiclePart> VehiclePart { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<BugReport> BugReports { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }

        public DbSet<StoreAddress> StoreAddress { get; set; }
        public DbSet<Puntuation> Puntuations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}