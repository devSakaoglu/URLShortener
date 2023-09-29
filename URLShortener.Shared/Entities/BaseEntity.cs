namespace URLShortener.Shared.Entities;

public abstract class BaseEntity<T>
{
    public T Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
}