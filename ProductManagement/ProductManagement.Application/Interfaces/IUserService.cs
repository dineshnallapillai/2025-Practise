using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserAsync(string username, string password);
    Task<User?> GetByUsernameAsync(string username);
    Task<User> CreateUserAsync(string username, string password, string role = "User");
}