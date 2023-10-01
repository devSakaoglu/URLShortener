namespace URLShortener.Shared.Models.Link;

public class GetByShortAddressModel
{
    public Guid UserId { get; set; }
    public string ShortAddress { get; set; }
}