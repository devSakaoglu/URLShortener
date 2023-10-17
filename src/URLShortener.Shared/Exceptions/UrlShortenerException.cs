namespace URLShortener.Shared.Exceptions;

public class UrlShortenerException : Exception
{
    // TODO do some logging here
    public UrlShortenerException()
    {

    }

    public UrlShortenerException(string message) : base(message)
    {

    }

    public UrlShortenerException(string message, Exception innerException) : base(message, innerException)
    {

    }
}