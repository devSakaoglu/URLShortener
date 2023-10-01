namespace URLShortener.Shared.Models.Link;

public class UpdateShortAddressModel
{
    public Guid UserId { get; set; }
    public string ShortAddress { get; set; }
    public long Id { get; set; }
}