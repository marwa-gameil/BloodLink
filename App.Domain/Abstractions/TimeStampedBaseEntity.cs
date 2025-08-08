namespace App.Domain.Abstractions;

public abstract class TimeStampedModel : BaseModel
{
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}

public abstract class ExtendedTimeStampedModel : TimeStampedModel
{
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
