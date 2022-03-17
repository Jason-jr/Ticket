namespace Domain.Common;

public abstract class AuditableEntity
{
    public int CreatedUser { get; set; }

    public DateTime CreatedTime { get; set; }

    public int UpdatedUser { get; set; }

    public DateTime UpdatedTime { get; set; }
}