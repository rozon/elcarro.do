namespace ElCarro.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ElCarro.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ElCarro.Web.Models.ApplicationDbContext context)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                try
                {
                    AddRoles(roleManager, Web.Constants.CompanyRole, Web.Constants.UserRole, Web.Constants.AdminRole);
                    AddUser("ElCarro", "elcarro.do@gmail.com", "_Welc0me1_", Web.Constants.AdminRole, userManager);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
                trans.Commit();
            }
        }

        private void AddUser(string username, string email, string password, string role, UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmail(email) != null)
                return;

            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true
            };
            var chkUser = userManager.Create(user, password);

            if (!chkUser.Succeeded)
                throw new Exception($"Error adding user: {username} in the seed. \n" + string.Join(", ", chkUser.Errors));

            var roleResult = userManager.AddToRole(user.Id, role);

            if (!roleResult.Succeeded)
                throw new Exception($"Error adding role {role} to user: {username} in the seed. \n" + string.Join(", ", roleResult.Errors));
        }

        private void AddRoles(RoleManager<IdentityRole> roleManager, params string[] rolesNames)
        {
            if (rolesNames == null || rolesNames.Count() == 0)
                return;
            rolesNames.ToList().ForEach(roleName =>
            {
                AddRole(roleManager, roleName);
            });
        }

        private void AddRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (roleManager.RoleExists(roleName))
                return;
            roleManager.Create(new IdentityRole()
            {
                Name = roleName
            });
        }
    }
}
