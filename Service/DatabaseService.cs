using LittleHelpers.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleHelpers.Service
{
    public class DatabaseService : IDatabaseService
    {
        private Models.AppContext context;
        private IWebHostEnvironment _env;

        public DatabaseService(Models.AppContext context, IWebHostEnvironment env)
        {
            this.context = context;
            _env = env;
        }

        public Task<List<Meal>> GetAllMealsAsync()
        {
            return context.Meals.Include(m=>m.MealIngredients).Include("MealIngredients.Ingredient").ToListAsync();
        }
        
        public Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return context.Ingredients.ToListAsync();
        }

        public void Add(Meal meal)
        {
            context.Meals.Add(meal);
            context.SaveChanges();
        }

        public async Task<Meal> CreateMealAsync(Meal item, Stream fileStream, string extension)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(uploadsFolder, fileName);

            using var fs = new FileStream(fullPath, FileMode.Create);
            await fileStream.CopyToAsync(fs);

            item.ImageUrl = $"/Images/{fileName}";

            context.Meals.Add(item);
            await context.SaveChangesAsync();

            return item;
        }

        public void Update(Meal meal)
        {
            var existing = context.Meals.FirstOrDefault(x => x.Id == meal.Id);
            if (existing != null)
            {
                existing.IsAirFryer = meal.IsAirFryer;
                existing.Description = meal.Description;
                existing.ImageUrl = meal.ImageUrl;
                existing.Name = meal.Name;
                existing.MealIngredients = meal.MealIngredients;
            }
            context.Entry(existing).State = EntityState.Modified;
            context.SaveChanges();
        }

    }
}
