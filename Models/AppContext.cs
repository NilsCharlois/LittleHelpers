using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Xml;

namespace LittleHelpers.Models
{
    public class AppContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public AppContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("LittleHelpers"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealIngredients>(
                builder =>
                {
                    builder.HasOne(mi => mi.Ingredient).WithMany(i => i.MealIngredients).HasForeignKey(mi => mi.IngredientId);
                    builder.HasOne(mi => mi.Meal).WithMany(i => i.MealIngredients).HasForeignKey(mi => mi.MealId);
                    builder.Property(mi => mi.Quantity).IsRequired();
                    builder.HasKey(mi=>mi.MealIngredientId);
                });
            modelBuilder.Entity<Meal>(builder =>
            {
                builder.HasMany(m => m.MealIngredients).WithOne(mi => mi.Meal);
                builder.ToTable("Meal");
            });
            modelBuilder.Entity<Ingredient>(builder =>
            {
                builder.HasMany(i => i.MealIngredients).WithOne(mi => mi.Ingredient);
                builder.ToTable("Ingredient");
            });
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MealIngredients> MealIngredients { get; set; }
    }
}
