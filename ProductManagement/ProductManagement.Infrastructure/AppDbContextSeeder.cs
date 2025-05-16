using ProductManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Persistence;

public static class AppDbContextSeeder
{
    public static async Task SeedAsync(AppDbContext context, ILogger logger)
    {
        if (await context.Products.AnyAsync())
        {
            logger.LogInformation("Database already seeded.");
            return;
        }

        logger.LogInformation("Seeding initial data...");

        var products = new List<Product>
        {
            new() { Name = "Laptop", /*Description = "High-performance laptop", */Price = 1499.99m },
            new() { Name = "Phone", /*Description = "Smartphone with 5G", */Price = 899.99m },
            new() { Name = "Headphones", /*Description = "Noise-cancelling headphones",*/ Price = 199.99m },
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();

        logger.LogInformation("Database seeded with sample products.");
    }
}