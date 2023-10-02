using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using URLShortener.Domain.Entities;
using URLShortener.Shared.Authentication;

namespace URLShortener.API.Authentication;

public class JwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateToken(AppUser appUser)
    {
        var claims = new Claim[]
        {
            new(ClaimTypes.Role, appUser.UserType.ToString()),
            new(JwtRegisteredClaimNames.Sid, appUser.Id.ToString())
        };

        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
                SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, null,
            DateTime.UtcNow.AddDays(7),
            signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }
}