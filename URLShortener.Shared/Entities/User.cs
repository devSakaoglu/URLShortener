using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using URLShortener.Shared.Enums;

namespace URLShortener.Shared.Entities;
public class User : BaseEntity<Guid>
{
    public UserType Type { get; set; }
    public List<Link> Links { get; set; }
}

