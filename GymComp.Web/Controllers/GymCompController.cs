using GymComp.Web.Models;
using System.Web.Mvc;

namespace GymComp.Web.Controllers
{
    public class GymCompController : Controller
    {
        protected ApplicationDbContext db;

        public GymCompController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}
