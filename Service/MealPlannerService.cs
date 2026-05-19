using LittleHelpers.Models;

namespace LittleHelpers.Service
{
    public class MealPlannerService : IMealPlannerService
    {
        private Models.AppContext _context;

        public MealPlannerService(Models.AppContext context)
        {
            _context = context;
        }

        public List<MealsForDay> GetAllPlannedDays()
        {
            return _context.MealsForDays.ToList();
        }

        public List<MealsForDay> GetMealsPlannedForDay(string date)
        {
            return _context.MealsForDays.Where(md=>md.Date.Equals(date)) .ToList();
        }
    }
}
