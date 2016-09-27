using ElCarro.Web.Models;
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
        public async Task<ActionResult> SendBugReport(BugReport model)
        {
            if (!ModelState.IsValid)
            {
                List<BugReportErrorView> listError = new List<BugReportErrorView>();

                for (int c = 0; c < ModelState.Count; c++)
                {
                    if (ModelState.Values.ElementAt(c).Errors.FirstOrDefault() != null)
                    {
                        listError.Add(new BugReportErrorView(ModelState.Keys.ElementAt(c),
                            ModelState.Values.ElementAt(c).Errors.FirstOrDefault().ErrorMessage));
                    }
                }

                return Json(new { status = "error", errors = listError });
            }

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
                        Body = model.Description,
                        Destination = model.Email,
                        Subject = "Report Error"
                    });
                });

            return Json(new { status = "success", message = "Thanks for the help!!" });
        }
    }
}