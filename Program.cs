using LittleHelpers.Components;
using LittleHelpers.Service;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("LittleHelpers");

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<LittleHelpers.Models.AppContext>(options => options.UseSqlite(connectionString).EnableDetailedErrors(), ServiceLifetime.Transient);
//builder.Services.AddBlazorBootstrap();
builder.Services.AddMudServices();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
