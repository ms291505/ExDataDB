using ExData.Data;
using ExData.Data.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowViteDev",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }
    );
});

builder.Services.AddDbContext<ExDataDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

app.UseCors("AllowViteDev");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExDataDb>();
    await LoadExercisesJson.InsertExercises(db);
}

app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", () => new { message = "Hello!" });

app.Run();
