namespace URLShortener.Shared.Models.Visit;

public class UpdateGeoDataModel
{
    public long VisitId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}