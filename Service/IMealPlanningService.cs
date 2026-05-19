using LittleHelpers.Models;

namespace LittleHelpers.Service
{
    public interface IMealPlannerService
    {
        public List<MealsForDay> GetAllPlannedDays();
        public List<MealsForDay> GetMealsPlannedForDay(DateTime date);
    }
}
