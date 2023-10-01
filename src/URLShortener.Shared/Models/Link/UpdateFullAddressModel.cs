namespace URLShortener.Shared.Models.Link;

public class UpdateFullAddressModel
{
    public Guid UserId { get; set; }
    public long Id { get; set; }
    public string FullAddress { get; set; }
}