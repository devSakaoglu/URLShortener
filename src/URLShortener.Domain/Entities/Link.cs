using System.ComponentModel.DataAnnotations;

namespace URLShortener.Domain.Entities;

public class Link : BaseEntity<long>
{
    [StringLength(10, MinimumLength = 10)]
    public string ShortAddress { get; set; }
    public string FullAddress { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsEnabled { get; set; } = true;
    public List<Visit> Visits { get; set; }
    public Guid UserId { get; set; }
    public AppUser AppUser { get; set; }
}