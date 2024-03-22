using Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/product")]
public class ProductController(IProductService _productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllProducts(cancellationToken);

        return Ok(products);
    }
}
