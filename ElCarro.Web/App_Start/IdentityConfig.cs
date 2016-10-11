using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ElCarro.Web.Models;
using SendGrid;
using System.Configuration;
using SendGrid.Helpers.Mail;
using System.Linq;
using ElCarro.Web.StringResource;

namespace ElCarro.Web
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            if (message.Body.Contains(HelperString.ConfirmEmail))
            {
                await configSendGridAsync(message);
            }
            if (message.Subject.Contains(HelperString.BugReport) ||
                message.Subject.Contains(HelperString.Suggestion))
            {
                sendMessageToAdmins(message);
            }
        }

        private void sendMessageToAdmins(IdentityMessage message)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var role = context.Roles.SingleOrDefault(m => m.Name.Equals(Constants.AdminRole));
            var users = context.Users.Where(m => m.Roles.Any(r => r.RoleId == role.Id));

            string apiKey = ConfigurationManager.AppSettings["SENDGRID_APY_KEY"];
            dynamic sg = new SendGridAPIClient(apiKey, "https://api.sendgrid.com");

            Email from = new Email(message.Destination);
            Content content = new Content("text/plain", message.Body);

            Parallel.ForEach(users, (user) =>
            {
                Email to = new Email(user.Email);
                Mail mail = new Mail(from, message.Subject, to, content);
                dynamic response = sg.client.mail.send.post(requestBody: mail.Get());
            });
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridAsync(IdentityMessage message)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = await UserManager.FindByEmailAsync(message.Destination);
            var roleCompany = await roleManager.FindByNameAsync(Constants.CompanyRole);

            if (user.Roles.FirstOrDefault(r => r.RoleId.Equals(roleCompany.Id)) == null)
            {
                string apiKey = ConfigurationManager.AppSettings["SENDGRID_APY_KEY"];
                dynamic sg = new SendGridAPIClient(apiKey, "https://api.sendgrid.com");

                Email from = new Email("elcarro.do@gmail.com");
                Email to = new Email(message.Destination);
                Content content = new Content("text/plain", message.Body);
                Mail mail = new Mail(from, message.Subject, to, content);
                dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
            }
            else
            {
                string apiKey = ConfigurationManager.AppSettings["SENDGRID_APY_KEY"];
                dynamic sg = new SendGridAPIClient(apiKey, "https://api.sendgrid.com");

                string subject = "Confirm Company Data";

                message.Body += "\t\r\t\r\t\r" + "Company Name: " + user.UserName + "\t\r\t\r" +
                    "Phone Numbre: " + user.PhoneNumber;

                Email from = new Email(message.Destination);
                Email to = new Email("elcarro.do@gmail.com");
                Content content = new Content("text/plain", message.Body);
                Mail mail = new Mail(from, subject, to, content);
                dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
