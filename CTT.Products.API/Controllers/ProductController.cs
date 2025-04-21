using CTT.Products.API.DTOs.Requests;
using CTT.Products.Business.ProductService.Interface;
using Microsoft.AspNetCore.Mvc;
using Products.Domain;

namespace CTT.Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("RegisterProduct")]
    public async Task<IActionResult> RegisterProduct([FromBody] RegisterProductRequest productRequest)
    {
        if (productRequest == null || string.IsNullOrWhiteSpace(productRequest.Description))
        {
            return BadRequest("Invalid product data.");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Description = productRequest.Description,
            Price = productRequest.Price,
            Stock = productRequest.Stock,
            Categories = productRequest.Categories
        };

        var createdProduct = await _productService.RegisterProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpGet("GetProductById/{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound("Product not found.");
        }

        return Ok(product);
    }
}
