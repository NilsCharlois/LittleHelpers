using LittleHelpers.Models;

namespace LittleHelpers.Service
{
    public interface IMealService
    {
        Task<Meal> CreateMealAsync(Meal item, Stream fileStream, string extension);
        Task<List<Meal>> SearchMeals(string value);
        void Update(Meal meal, Stream? newFileStream = null, string? extension = null);
        void Delete(Meal meal);
        Task<List<Meal>> GetAllMealsAsync();
    }
}
