namespace URLShortener.Domain.Entities;

public class Visit : BaseEntity<long>
{
    public string IpAddress { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public long LinkId { get; set; }
    public Link Link { get; set; }
}