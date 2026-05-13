using LittleHelpers.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleHelpers.Service
{
    public class MealService : IMealService
    {
        private Models.AppContext context;
        private IWebHostEnvironment _env;

        public MealService(Models.AppContext context, IWebHostEnvironment env)
        {
            this.context = context;
            _env = env;
        }

        public Task<List<Meal>> GetAllMealsAsync()
        {
            return context.Meals.Include(m => m.MealIngredients).ThenInclude(x => x.Ingredient).ToListAsync();
        }

        public async Task<Meal> CreateMealAsync(Meal item, Stream fileStream, string extension)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = await SaveFile(fileStream, extension);

            item.ImageName = fileName;

            context.Meals.Add(item);
            await context.SaveChangesAsync();

            return item;
        }

        public async void Update(Meal meal, Stream? newFileStream = null, string? extension = null)
        {
            var existing = context.Meals.FirstOrDefault(x => x.Id == meal.Id);

            if (existing == null)
                return;

            existing.Name = meal.Name;
            existing.Description = meal.Description;
            existing.MealIngredients = meal.MealIngredients;
            existing.IsAirFryer = meal.IsAirFryer;

            // New image uploaded
            if (newFileStream != null && extension != null)
            {
                var oldImagePath = existing.ImageName;

                // Save new image first
                var newRelativePath = await SaveFile(newFileStream, extension);

                // Update DB
                existing.ImageName = newRelativePath;

                context.SaveChanges();

                // Delete old image AFTER success
                DeleteImage(oldImagePath);
            }
            else
            {
                context.SaveChanges();
            }
        }

        private async Task<string> SaveFile(Stream stream, string extension)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{extension}";

            var fullPath = Path.Combine(uploadsFolder, fileName);

            using var fs = new FileStream(fullPath, FileMode.Create);

            await stream.CopyToAsync(fs);

            return fileName;
        }

        public void Delete(Meal meal)
        {
            var existing = context.Meals.FirstOrDefault(x => x.Id == meal.Id);
            if (existing != null)
            {
                DeleteImage(existing.ImageName);
                context.Entry(existing).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private void DeleteImage(string fileName)
        {
            var fullPath = Path.Combine(_env.WebRootPath, "Images", fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
