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

        public MealsForDay GetMealsPlannedForDay(string date)
        {
            return _context.MealsForDays.SingleOrDefault(md=>md.Date.Equals(date));
        }

        public async Task<MealsForDay> CreateMealsForDayAsync(MealsForDay model)
        {
            _context.MealsForDays.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async void Update(MealsForDay model)
        {
            var existing = _context.MealsForDays.FirstOrDefault(x => x.MealsForDayId == model.MealsForDayId);

            if (existing == null)
                return;

            existing.Date = model.Date;
            existing.LunchMealId = model.LunchMealId;
            existing.SnackMealId = model.SnackMealId;
            existing.DinnerMealId = model.DinnerMealId;

                _context.SaveChanges();
            
        }
    }
}
