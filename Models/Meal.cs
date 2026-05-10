namespace LittleHelpers.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public bool IsAirFryer { get; set; } = false;
        public List<MealIngredients> MealIngredients { get; set; } = new();

    }
}
