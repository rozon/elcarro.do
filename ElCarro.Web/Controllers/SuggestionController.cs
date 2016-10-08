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
    public class SuggestionController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context = new ApplicationDbContext();

        public SuggestionController()
        {
        }

        public SuggestionController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        public PartialViewResult Suggestion()
        {
            return PartialView("Suggestion");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SendSuggestion(Suggestion model)
        {
            if (!ModelState.IsValid)
                return Json(new { status = Messages.error, message = Messages.Error002 });

            try
            {
                Parallel.Invoke(
                    () =>
                    {
                        context.Suggestions.Add(model);
                        context.SaveChangesAsync();
                    },
                    () =>
                    {
                        UserManager.EmailService.SendAsync(new IdentityMessage()
                        {
                            Body = model.SuggestionMsj,
                            Destination = model.EmailSug,
                            Subject = HelperString.Suggestion
                        });
                    });
            }
            catch (Exception)
            {
                return Json(new { status = Messages.error, message = Messages.Error001 });
            }

            return Json(new { status = Messages.success, message = Messages.ResMsjSugerencia });
        }
    }
}