namespace URLShortener.Shared.Entities;

public class Visit : BaseEntity<long>
{
    public String IpAddress { get; set; }
    public String Country { get; set; }
    public String City { get; set; }
}