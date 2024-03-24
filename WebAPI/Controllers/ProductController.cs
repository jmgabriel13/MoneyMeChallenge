using Application.Products;
using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Update;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using WebApp.Abstraction;

namespace WebApp.Controllers;

[Route("api/products")]
public sealed class ProductController(IProductService _productService) : ApiController
{
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        Result result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        Result result = await _productService.GetAllProducts(cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpPut]
    [Route("update/{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(
            id,
            request.Name,
            request.PerAnnumInterestRate,
            request.MinimumDuration,
            request.MonthsOfFreeInterest,
            request.EstablishmentFee);

        Result result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }


    [HttpDelete]
    [Route("delete/{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        Result result = await Mediator.Send(new DeleteProductCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
