using System;

namespace GymComp.Web.Models
{
    public class NumberOfWorkoutsPerWeekDayOfUser
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string User { get; set; }
        public int Workouts { get; set; }
    }
}