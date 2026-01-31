using System.Text.Json;
using ExData.Models;
using Microsoft.EntityFrameworkCore;

namespace ExData.Data.Seed;

public static class LoadExercisesJson
{
    public static readonly string _exercisesJson = "Data/Seed/exercises.json";

    public static async Task<List<Exercise>> ReadExercises()
    {
        List<Exercise> exercises = [];
        var json = await File.ReadAllTextAsync(_exercisesJson);

        var rawExercises = JsonSerializer.Deserialize<List<RawExercise>>(json);
        if (rawExercises is null)
            return exercises;

        foreach (var raw in rawExercises)
        {
            var exercise = new Exercise
            {
                Name = raw.name,
                Force = raw.force,
                Mechanic = raw.mechanic,
                PrimaryMuscles = raw.primaryMuscles.FirstOrDefault() ?? "",
                SecondaryMuscles = raw.secondaryMuscles.FirstOrDefault() ?? "",
                Equipment = raw.equipment,
                Category = raw.category,
                CreatedBy = "system",
            };

            exercises.Add(exercise);
        }

        return exercises;
    }

    public static async Task InsertExercises(ExDataDb db)
    {
        var existing = await db.Exercises.Where(e => e.CreatedBy == "system").ToListAsync();

        var exercises = await ReadExercises();

        foreach (var exercise in exercises)
        {
            bool exists = existing.Any(e => e.Name == exercise.Name);

            if (!exists)
                await db.Exercises.AddAsync(exercise);
        }

        await db.SaveChangesAsync();
    }

    public record RawExercise(
        string id,
        string name,
        string? force,
        string? mechanic,
        string equipment,
        List<string> primaryMuscles,
        List<string> secondaryMuscles,
        List<string> instructions,
        string category,
        List<string> images
    );
}
