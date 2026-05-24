using LittleHelpers.Models;

namespace LittleHelpers.Service
{
    public interface IMealPlannerService
    {
        public List<MealsForDay> GetAllPlannedDays();
        public MealsForDay GetMealsPlannedForDay(string date);
        public Task<MealsForDay> CreateMealsForDayAsync(MealsForDay model);
        public void Update(MealsForDay model);
    }
}
