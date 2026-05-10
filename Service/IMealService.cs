using LittleHelpers.Models;

namespace LittleHelpers.Service
{
    public interface IMealService
    {
        Task<Meal> CreateMealAsync(Meal item, Stream fileStream, string extension);
        //void Update(Meal meal);
        void Update(Meal meal, Stream? newFileStream = null, string? extension = null);
        void Delete(Meal meal);
        Task<List<Meal>> GetAllMealsAsync();
    }
}
