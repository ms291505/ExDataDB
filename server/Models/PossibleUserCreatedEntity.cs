namespace ExData.Models;

public abstract class TrackedEntity
{
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

    public required string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public void SoftDelete()
    {
        DeletedAt = DateTimeOffset.UtcNow;
    }
}
