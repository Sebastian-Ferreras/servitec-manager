namespace ServiTec.Domain.Exceptions;

/// <summary>The request conflicts with the current state (e.g. duplicated unique value). Mapped to HTTP 409.</summary>
public class ConflictException(string message) : Exception(message);
