using ElCarro.Web.Models;
using ElCarro.Web.StringResource;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ElCarro.Web.Controllers
{
    [Authorize]
    public class BugReportController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context = new ApplicationDbContext();

        public BugReportController()
        {
        }

        public BugReportController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
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

        [AllowAnonymous]
        public PartialViewResult BugReport()
        {
            return PartialView("BugsReport");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SendBugReport(BugReport model)
        {
            if (!ModelState.IsValid)
                return Json(new { status = Messages.error, message = Messages.Error002 });

            try
            {
                Parallel.Invoke(
                    () =>
                    {
                        context.BugReports.Add(model);
                        context.SaveChangesAsync();
                    },
                    () =>
                    {
                        UserManager.EmailService.SendAsync(new IdentityMessage()
                        {
                            Body = model.DescriptionBR,
                            Destination = model.EmailBR,
                            Subject = HelperString.BugReport
                        });
                    });
            }
            catch (Exception)
            {
                return Json(new { status = Messages.error, message = Messages.Error001 });
            }

            return Json(new { status = Messages.success, message = Messages.ResMsjBugReport });
        }
    }
}
