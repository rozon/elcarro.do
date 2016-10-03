using ElCarro.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
            {
                List<SuggestionErrorView> listError = new List<SuggestionErrorView>();

                for (int c = 0; c < ModelState.Count; c++)
                {
                    if (ModelState.Values.ElementAt(c).Errors.FirstOrDefault() != null)
                    {
                        listError.Add(new SuggestionErrorView(ModelState.Keys.ElementAt(c),
                            ModelState.Values.ElementAt(c).Errors.FirstOrDefault().ErrorMessage));
                    }
                }

                return Json(new { status = "error", errors = listError });
            }

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
                        Destination = model.Email,
                        Subject = "Suggestion"
                    });
                });

            return Json(new { status = "success", message = "Thanks for the Suggestion!!" });
        }
    }
}