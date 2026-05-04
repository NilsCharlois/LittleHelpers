using LittleHelpers.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleHelpers.Service
{
    public class DatabaseService : IDatabaseService
    {
        private Models.AppContext context;
        public DatabaseService(Models.AppContext context)
        {
            this.context = context;
        }

        public Task<List<Meal>> GetAllMealsAsync()
        {
            return context.Meals.Include(m=>m.MealIngredients).Include("MealIngredients.Ingredient").ToListAsync();
        }
        
        public Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return context.Ingredients.ToListAsync();
        }
    }
}
