namespace ElCarro.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ElCarro.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // In this method we will create default User roles and Admin user for login
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists(Web.Constants.AdminRole))
            {

                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = Web.Constants.AdminRole;
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "ElCarro";
                user.Email = "elcarro.do@gmail.com";
                user.EmailConfirmed = true;

                string userPWD = "_Welc0me1_";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, Web.Constants.AdminRole);
                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists(Web.Constants.CompanyRole))
            {
                var role = new IdentityRole();
                role.Name = Web.Constants.CompanyRole;
                roleManager.Create(role);
            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists(Web.Constants.UserRole))
            {
                var role = new IdentityRole();
                role.Name = Web.Constants.UserRole;
                roleManager.Create(role);

            }
    }
    }
}
