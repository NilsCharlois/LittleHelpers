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
            throw new NotImplementedException();
        }

        public List<MealsForDay> GetMealsPlannedForDay(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
