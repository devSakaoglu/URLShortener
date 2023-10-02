namespace URLShortener.Shared.Authentication;

public class JwtOptions
{
    public JwtOptions()
    {
        Secret = Environment.GetEnvironmentVariable("JWT_KEY_US");
    }

    public string Issuer { get; init; }
    public string Audience { get; init; }

    public string Secret { get; private set; }
}