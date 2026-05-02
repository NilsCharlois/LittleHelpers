using LittleHelpers.Models;

namespace LittleHelpers.Data
{
    public interface IDatabaseService
    {
        Task<List<Meal>> GetAllMealsAsync();
        Task<List<Ingredient>> GetAllIngredientsAsync();
    }
}
