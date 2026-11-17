using ServiTec.Application.DTOs.Common.Auth;
using ServiTec.Domain.Entities;

namespace ServiTec.Application.Interfaces;

public interface ITokenService
{
    TokenResult GenerateToken(User user);
}
