namespace URLShortener.Shared.Entities;

public class Link: BaseEntity<long>
{
    public string ShortAddress { get; set; }
    public string FullAddress { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsEnabled { get; set; }
    public List<Visit> Visits { get; set; }
}