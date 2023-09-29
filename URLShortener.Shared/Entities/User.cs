using URLShortener.Shared.Enums;

namespace URLShortener.Shared.Entities;

public class User : BaseEntity<Guid>
{
    public String Email { get; set; }
    //Will be added password prop
    public UserType Type { get; set; }
        
}

