using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PorPartes.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here
            // Configure User as FK for Address
            modelBuilder.Entity<Address>()
                        .HasOptional(a => a.User)
                        .WithRequired(u => u.Address);

            // Configure Shop as FK for Address
            modelBuilder.Entity<Address>()
                        .HasOptional(a => a.Shop)
                        .WithRequired(u => u.Address);

            // Configure Adverising as FK for Address
            modelBuilder.Entity<Address>()
                        .HasOptional(a => a.Advertising)
                        .WithRequired(u => u.Address);

            // Configure Stree as FK for Address
            modelBuilder.Entity<Address>()
                        .HasRequired(a => a.Street)
                        .WithRequiredPrincipal(u => u.Address);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FrequentQuestion> FrequentQuestions { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<ModelVehicle> ModelVehicles { get; set; }
        public DbSet<Puntuation> Puntuations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<Street> Streets { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
    }
}