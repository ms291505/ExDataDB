using ExData.Api.Endpoints;
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

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("https://exdatadb-production.up.railway.app/")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }
    );
});

builder.Services.AddDbContext<ExDataDb>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    }
    else
    {
        var host = Environment.GetEnvironmentVariable("PGHOST");
        var port = Environment.GetEnvironmentVariable("PGPORT");
        var db = Environment.GetEnvironmentVariable("PGDATABASE");
        var user = Environment.GetEnvironmentVariable("PGUSER");
        var pass = Environment.GetEnvironmentVariable("PGPASSWORD");

        var connectionString =
            $"Host={host};Port={port};Database={db};Username={user};Password={pass};SSL Mode=Require;Trust Server Certificate=true";
        options.UseNpgsql(connectionString);
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowViteDev");
}
else
{
    app.UseCors("AllowFrontend");
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExDataDb>();
    await db.Database.MigrateAsync();
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
api.MapExerciseEndpoints();

var port = Environment.GetEnvironmentVariable("PGPORT") ?? "5192";
app.Urls.Add($"http://*:{port}");

app.Run();
