namespace LittleHelpers.Models
{
    public class MealsForDay
    {
        public int? LunchMealId { get; set; }
        public virtual Meal? LunchMeal { get; set; }

        public int? DinnerMealId { get; set; }
        public virtual Meal? DinnerMeal { get; set; }

        public DateTime Date { get; set; }
    }
}
