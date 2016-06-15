using GymComp.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace GymComp.Web.Controllers
{
    public class HomeController : GymCompController
    {
        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var displayName = db.Users.FirstOrDefault(u => u.UserName.Equals(userName)).DisplayName;
                ViewData["DisplayName"] = displayName;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}