using jex_assessment.Data;
using jex_assessment.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database and app services via extension methods
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddAppServices();

// CORS policy (open for dev; tighten as needed)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AssessmentDbContext>();
        await context.Database.MigrateAsync(); // Apply migrations
        await services.SeedDataAsync(); // Seed data
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        throw;
    }
}

// Configure pipeline and SPA fallback
app.ConfigurePipeline().ConfigureSpaFallback();

app.Run();
