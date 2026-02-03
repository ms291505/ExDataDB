namespace ExData.Models;

public class Exercise : TrackedEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Force { get; set; }
    public string? Mechanic { get; set; }
    public required string PrimaryMuscles { get; set; }
    public required string SecondaryMuscles { get; set; }
    public string? Equipment { get; set; }
    public required string Category { get; set; }
}
