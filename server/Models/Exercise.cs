namespace ExData.Models;

public class Exercise : PossibleUserCreatedEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
