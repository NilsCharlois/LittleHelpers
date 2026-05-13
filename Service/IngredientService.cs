using LittleHelpers.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleHelpers.Service
{
    public class IngredientService : IIngredientService
    {
        private Models.AppContext _context;
        public IngredientService(Models.AppContext context)
        {
            _context = context;
        }

        public async Task<List<Ingredient>> SearchIngredients(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return await _context.Ingredients.OrderBy(i => i.Name).Take(20).ToListAsync();
            }

            return await _context.Ingredients.Where(i => i.Name.Contains(value)).OrderBy(i => i.Name).Take(20).ToListAsync();
        }

        public async Task<List<Ingredient>> GetAll()
        {
            return await _context.Ingredients.OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<Ingredient> GetOrCreate(string name)
        {
            name = name.Trim();

            var existing = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower());

            if (existing != null)
                return existing;

            var ingredient = new Ingredient
            {
                Name = name
            };

            _context.Ingredients.Add(ingredient);

            await _context.SaveChangesAsync();

            return ingredient;
        }

    }
}
