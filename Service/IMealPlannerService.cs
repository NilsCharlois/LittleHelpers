using LittleHelpers.Models;

namespace LittleHelpers.Service
{
    public interface IMealPlannerService
    {
        public void RemoveAllMealsForDay(string date);
        public List<MealsForDay> GetAllPlannedLunches();
        public List<MealsForDay> GetAllPlannedSnacks();
        List<MealsForDay> GetAllPlannedDinners();
        public List<MealsForDay> GetAllPlannedDays();
        public MealsForDay GetMealsPlannedForDay(string date);
        public void CreateMealsForDay(MealsForDay model);
        public void Update(MealsForDay model);
    }
}
