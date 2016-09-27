using ElCarro.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace ElCarro.Web.Controllers
{
    public class PartialViewController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public PartialViewController()
        {
        }

        public PartialViewController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult userView()
        {
            var user = UserManager.FindByEmail(User.Identity.Name);
            bool isCompany = false;

            if (User.IsInRole(Constants.CompanyRole))
                isCompany = true;

            var result = new UserView
            {
                FullName = user.FullName,
                Email = user.Email,
                isCompany = isCompany
            };

            return PartialView("userView", result);
        }
    }
}