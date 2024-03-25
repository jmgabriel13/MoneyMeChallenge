using Application.Interfaces;
using Domain.Entities.Products;
using Domain.Shared;

namespace Application.Products.Create;
internal sealed class CreateProductCommandHandler(
    IProductRepository _productRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<CreateProductCommand>
{
    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            Guid.NewGuid(),
            request.Name,
            request.PerAnnumInterestRate,
            request.MinimumDuration,
            request.MonthsOfFreeInterest,
            request.EstablishmentFee);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
