namespace URLShortener.Shared.Models.Link;

public class SetEnabledModel
{
    public Guid UserId { get; set; }
    public long Id { get; set; }
    public bool IsEnabled { get; set; }
}