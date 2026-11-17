using Microsoft.EntityFrameworkCore;
using ServiTec.Application.Interfaces;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
}
