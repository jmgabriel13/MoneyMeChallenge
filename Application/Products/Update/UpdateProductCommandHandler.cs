using Application.Interfaces;
using Domain.Errors;
using Domain.Shared;

namespace Application.Products.Update;
internal sealed class UpdateProductCommandHandler(
    IProductRepository _productRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure(DomainErrors.Product.ProductNotFound(request.Id));
        }

        product.Update(
            request.Name,
            request.PerAnnumInterestRate,
            request.MinimumDuration,
            request.MonthsOfFreeInterest);

        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
