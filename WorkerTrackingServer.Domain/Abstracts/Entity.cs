namespace WorkerTrackingServer.Domain.Abstract;
public class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return Id == entity.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
