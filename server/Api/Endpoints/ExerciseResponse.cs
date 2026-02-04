namespace ExData.Api.Endpoints;

public sealed record ExerciseResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string? Force { get; init; }
    public string? Mechanic { get; init; }
    public string PrimaryMuscles { get; init; } = default!;
    public string SecondaryMuscles { get; init; } = default!;
    public string? Equipment { get; init; }
    public string Category { get; init; } = default!;
}
