namespace URLShortener.Shared.Models.Link;

public class GetByIdModel
{
    public Guid UserId { get; set; }
    public long Id { get; set; }
}