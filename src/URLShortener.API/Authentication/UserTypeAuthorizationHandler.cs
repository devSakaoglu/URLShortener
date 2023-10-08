using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using URLShortener.Domain.Enums;

namespace URLShortener.API.Authentication;

public class UserTypeAuthorizationHandler : AuthorizationHandler<UserTypeAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        UserTypeAuthorizationRequirement requirement)
    {
        var userType = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        if (!Enum.TryParse(userType, true, out UserType result)) return Task.CompletedTask;

        if (result >= requirement.UserType) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}