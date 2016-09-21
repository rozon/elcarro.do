using System.Web.Mvc;

namespace ElCarro.Web.Controllers
{
    public class ModalsController : Controller
    {
        // GET: Modals
        public PartialViewResult PrivacyPolicy()
        {
            return PartialView("PrivacyPolicy");
        }

        public PartialViewResult TermsConditions()
        {
            return PartialView("TermsConditions");
        }

        public PartialViewResult Rules()
        {
            return PartialView("Rules");
        }

        public PartialViewResult Suggestions()
        {
            return PartialView("Suggestions");
        }
    }
}
