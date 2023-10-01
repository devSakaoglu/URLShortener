namespace URLShortener.Shared.Models.Link;

public class CreateLinkModel
{
    public string ShortAddress { get; set; }
    public string FullAddress { get; set; }
    public Guid UserId { get; set; }
}