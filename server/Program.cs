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

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowViteDev");
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExDataDb>();
    await LoadExercisesJson.InsertExercises(db);
}
var api = app.MapGroup("/api");

api.MapGet("/", () => "Hello World!");
api.MapGet("/hello", () => TypedResults.Ok(new { message = "Hello!" }));
api.MapGet("/not-found", () => TypedResults.NotFound(new { message = "Not Found At All!" }));
api.MapGet("/bad-response", () => TypedResults.BadRequest());
api.MapGet("/unauthorized", () => TypedResults.Unauthorized());
api.MapGet("/forbidden", () => TypedResults.Forbid());
api.MapGet("/created", () => TypedResults.Created());
api.MapGet("/conflict", () => TypedResults.Conflict());

app.Run();
