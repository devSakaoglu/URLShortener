namespace URLShortener.Shared.Models.Visit;

public class CreateVisitModel
{
    public long LinkId { get; set; }
    public string IpAddress { get; set; }

    public string Country { get; set; }

    public string City { get; set; }
}