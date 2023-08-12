namespace KeyvanSafe.Shared.EntityFramework.Entities;

public interface IBaseEntity
{
}
public abstract class BaseEntity<T> : IBaseEntity
{
    public T Id { get; set; }
    public int CreatorId { get; set; }
    public int UpdaterId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}