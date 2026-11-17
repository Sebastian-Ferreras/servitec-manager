namespace ServiTec.Domain.Exceptions;

/// <summary>The request is well-formed but violates a business rule. Mapped to HTTP 422.</summary>
public class BusinessRuleException(string message) : Exception(message);
