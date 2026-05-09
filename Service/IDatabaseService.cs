using LittleHelpers.Models;

namespace LittleHelpers.Service
{
    public interface IDatabaseService
    {
        void Add(Meal meal);
        Task<Meal> CreateMealAsync(Meal item, Stream fileStream, string extension);
        void Update(Meal meal);
        Task<List<Meal>> GetAllMealsAsync();
        Task<List<Ingredient>> GetAllIngredientsAsync();
    }
}
