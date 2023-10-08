using Microsoft.AspNetCore.Authorization;
using URLShortener.Domain.Enums;

namespace URLShortener.API.Authentication;

public class UserTypeAuthorizationRequirement : IAuthorizationRequirement
{
    public readonly UserType UserType;

    public UserTypeAuthorizationRequirement(UserType userType)
    {
        UserType = userType;
    }
}