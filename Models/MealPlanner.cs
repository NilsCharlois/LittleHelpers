using System.ComponentModel.DataAnnotations.Schema;

namespace LittleHelpers.Models
{
    public class MealsForDay
    {
        public int MealsForDayId { get; set; }
        public int? LunchMealId { get; set; }
        public virtual Meal? LunchMeal { get; set; }

        public int? DinnerMealId { get; set; }
        public virtual Meal? DinnerMeal { get; set; }

        public string Date { get; set; } // format yyyy/MM/dd
    }
}
