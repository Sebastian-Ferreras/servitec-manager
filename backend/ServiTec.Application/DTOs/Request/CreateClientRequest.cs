using System.ComponentModel.DataAnnotations;

namespace ServiTec.Application.DTOs.Request;

public class CreateClientRequest
{
    [Required]
    [MaxLength(150)]
    public string FullName { get; set;} = string.Empty;

    [MaxLength(254)]
    public string? Email { get; set;} 

    [Required]
    public List<string> Phones { get; set;} = [];
}