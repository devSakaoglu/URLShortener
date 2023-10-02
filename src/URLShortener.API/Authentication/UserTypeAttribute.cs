using Microsoft.AspNetCore.Authorization;
using URLShortener.Domain.Enums;

namespace URLShortener.API.Authentication;

public class UserTypeAttribute : AuthorizeAttribute
{
    public UserTypeAttribute(UserType userType) : base(userType.ToString())
    {
    }
}