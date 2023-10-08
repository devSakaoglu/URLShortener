using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using URLShortener.Domain.Enums;

namespace URLShortener.API.Authentication;

public class UserTypeAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public UserTypeAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy is not null) return policy;

        if (!Enum.TryParse(policyName, true, out UserType result)) return null;

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new UserTypeAuthorizationRequirement(result))
            .Build();
    }
}