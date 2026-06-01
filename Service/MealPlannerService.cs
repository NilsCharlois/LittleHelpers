using LittleHelpers.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleHelpers.Service
{
    public class MealPlannerService : IMealPlannerService
    {
        private Models.AppContext _context;

        public MealPlannerService(Models.AppContext context)
        {
            _context = context;
        }

        public void RemoveAllMealsForDay(string date)
        {
            MealsForDay mealsForDay = _context.MealsForDays.SingleOrDefault(m => m.Date.Equals(date));
            if (mealsForDay != null)
            {
                _context.Entry(mealsForDay).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }

        public List<MealsForDay> GetAllPlannedLunches()
        {
            return _context.MealsForDays
                .Include(m => m.LunchMeal)
                .Where(m => !string.IsNullOrEmpty(m.Date) && m.LunchMealId.HasValue)
                .ToList();
        }

        public List<MealsForDay> GetAllPlannedSnacks()
        {
            return _context.MealsForDays
                .Include(m => m.SnackMeal)
                .Where(m => !string.IsNullOrEmpty(m.Date) && m.SnackMealId.HasValue)
                .ToList();
        }

        public List<MealsForDay> GetAllPlannedDinners()
        {
            return _context.MealsForDays
                .Include(m => m.DinnerMeal)
                .Where(m => !string.IsNullOrEmpty(m.Date) && m.DinnerMealId.HasValue)
                .ToList();
        }

        public List<MealsForDay> GetAllPlannedDays()
        {
            return _context.MealsForDays
                .Include(m=>m.LunchMeal)
                .Include(m=>m.SnackMeal)
                .Include(m=>m.DinnerMeal)
                .Where(m=>!string.IsNullOrEmpty(m.Date) && (m.LunchMealId.HasValue || m.SnackMealId.HasValue || m.DinnerMealId.HasValue))
                .ToList();
        }

        public MealsForDay GetMealsPlannedForDay(string date)
        {
            return _context.MealsForDays
                .Include(m => m.LunchMeal)
                .Include(m => m.SnackMeal)
                .Include(m => m.DinnerMeal)
                .SingleOrDefault(md=>md.Date.Equals(date));
        }

        public void CreateMealsForDay(MealsForDay model)
        {
            _context.MealsForDays.Add(model);
            _context.SaveChanges();
        }

        public async void Update(MealsForDay model)
        {
            var existing = _context.MealsForDays.FirstOrDefault(x => x.MealsForDayId == model.MealsForDayId);

            if (existing == null)
                return;

            existing.Date = model.Date;
            existing.LunchMealId = model.LunchMealId;
            existing.LunchMeal = model.LunchMeal;
            existing.SnackMealId = model.SnackMealId;
            existing.SnackMeal = model.SnackMeal;
            existing.DinnerMealId = model.DinnerMealId;
            existing.DinnerMeal = model.DinnerMeal;

            _context.SaveChanges();
        }
    }
}
