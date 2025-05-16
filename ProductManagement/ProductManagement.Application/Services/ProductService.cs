using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return _context.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        public void Add(ProductDto product)
        {
            _context.Products.Add(new Product
            {
                Name = product.Name,
                Price = product.Price
            });
            _context.SaveChanges();
        }
    }
}