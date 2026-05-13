using LittleHelpers.Models;

namespace LittleHelpers.Service
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> SearchIngredients(string value);
        Task<List<Ingredient>> GetAll();
        Task<Ingredient> GetOrCreate(string name);
    }
}
