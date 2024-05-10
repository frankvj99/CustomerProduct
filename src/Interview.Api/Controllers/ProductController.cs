using System.Collections.Generic;
using System.Threading.Tasks;
using Interview.Data.Models;
using Interview.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Api.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdateProduct(Product product)
    {
        await _productService.AddOrUpdateProductAsync(product);

        return Ok();
    }

    // GET: api/product/{id}
    [HttpGet("id")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productService.GetProductAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}
