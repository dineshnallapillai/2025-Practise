using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Get() => Ok(_productService.GetAll());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProduct([FromBody] ProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _productService.Add(product);
            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
        }
    }
}