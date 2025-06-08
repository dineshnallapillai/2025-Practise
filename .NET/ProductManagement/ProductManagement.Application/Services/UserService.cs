using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using System.Security.Cryptography;
using System.Text;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetUserAsync(string username, string password)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
        if (user == null) return null;

        var hashed = HashPassword(password);
        return user.PasswordHash == hashed ? user : null;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> CreateUserAsync(string username, string password, string role = "User")
    {
        var user = new User
        {
            Username = username,
            PasswordHash = HashPassword(password),
            Role = role
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}