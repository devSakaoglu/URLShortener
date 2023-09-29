using URLShortener.Shared.Enums;

namespace URLShortener.Shared.Entities;
public class User : BaseEntity<Guid>
{
    public string Email { get; set; }
    //Will be added password prop
    public UserType Type { get; set; }
    public List<Link> Links { get; set; }
}

