using ProductManagement.Application.DTOs;
using System.Collections.Generic;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();
        void Add(ProductDto product);
    }
}