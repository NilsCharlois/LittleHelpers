using LittleHelpers.Models;

namespace LittleHelpers.Service
{
    public interface IDatabaseService
    {
        Task<List<Meal>> GetAllMealsAsync();
        Task<List<Ingredient>> GetAllIngredientsAsync();
    }
}
