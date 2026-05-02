namespace LittleHelpers.Models
{
    public class MealIngredients
    {
        public int MealIngredientId { get; set; }
        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }

        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        public string Quantity { get; set; }
    }
}
