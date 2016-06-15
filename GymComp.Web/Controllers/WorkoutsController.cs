using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GymComp.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;

namespace GymComp.Web.Controllers
{
    [Authorize]
    public class WorkoutsController : GymCompController
    {
        public PartialViewResult _Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkoutId, Trained")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                db.Users.First(u => u.UserName.Equals(userName)).Workouts.Add(workout);
                db.SaveChanges();
                TempData["WorkoutCreated"] = true;
            }

            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult _NumberOfWorkoutsPerWeekDayComparison(string compareWithUser)
        {
            var currentUser = db.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
            var userToCompare = db.Users.FirstOrDefault(u => u.UserName.Equals(compareWithUser));

            var currentUserWorkouts = db.Workouts.AsQueryable()
                .Where(w => w.ApplicationUserId == currentUser.Id)
                .GroupBy(w => SqlFunctions.DatePart("weekday", w.Trained))
                .Select(group => new NumberOfWorkoutsPerWeekDayOfUser
                {
                    User = currentUser.DisplayName,
                    DayOfWeek = (DayOfWeek) group.Key,
                    Workouts = group.Count()
                })
                .ToList();

            return PartialView(currentUserWorkouts);
        }

        [AllowAnonymous]
        public PartialViewResult _TopTrainersThisWeek()
        {
            var lastWeek = DateTime.Now.AddDays(-7);
            var workouts = db.Workouts.Where(w => w.Trained > lastWeek);
            var groupedByUser = workouts.GroupBy(w => w.ApplicationUserId).ToList();
            var model = new List<TopTrainerThisWeek>();

            foreach(var user in groupedByUser)
            {
                var workout = user.First();
                model.Add(new TopTrainerThisWeek {
                    Name = workout.ApplicationUser.DisplayName,
                    NumberOfWorkouts = user.Count()
                });
            }

            return PartialView(model.OrderByDescending(m => m.NumberOfWorkouts));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
