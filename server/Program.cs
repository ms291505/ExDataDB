using ExData.Data;
using ExData.Data.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExDataDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExDataDb>();
    await LoadExercisesJson.InsertExercises(db);
}

app.MapGet("/", () => "Hello World!");

app.Run();
