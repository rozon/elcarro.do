namespace PorPartes.DAL
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PorParteContext : DbContext
    {
        // Your context has been configured to use a 'PorParteContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PorPartes.DAL.PorParteContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PorParteContext' 
        // connection string in the application configuration file.
        public PorParteContext()
            : base("name=PorParteContext")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<PorParteContext>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here
            // Configure User as FK for Address
            //modelBuilder.Entity<Address>()
            //            .HasOptional(a => a.User)
            //            .WithRequired(u => u.Address);

            //// Configure Shop as FK for Address
            //modelBuilder.Entity<Address>()
            //            .HasOptional(a => a.Shop)
            //            .WithRequired(u => u.Address);

            //// Configure Adverising as FK for Address
            //modelBuilder.Entity<Address>()
            //            .HasOptional(a => a.Advertising)
            //            .WithRequired(u => u.Address);

            //// Configure Stree as FK for Address
            //modelBuilder.Entity<Address>()
            //            .HasRequired(a => a.Street)
            //            .WithRequiredPrincipal(u => u.Address);
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
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
    }
}
