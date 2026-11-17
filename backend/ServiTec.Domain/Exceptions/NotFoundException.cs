namespace ServiTec.Domain.Exceptions;

/// <summary>The requested resource does not exist. Mapped to HTTP 404.</summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string resourceName, object key)
        : base($"{resourceName} with id '{key}' was not found.")
    {
    }
}
