namespace ServiTec.Domain.Exceptions;

/// <summary>The caller is not authenticated or the credentials are invalid. Mapped to HTTP 401.</summary>
public class UnauthorizedException(string message = "Invalid credentials.") : Exception(message);
