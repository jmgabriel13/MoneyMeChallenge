using Application.Interfaces;
using Domain.Errors;
using Domain.Shared;

namespace Application.Products.Delete;
internal sealed class DeleteProductCommandHandler(
    IProductRepository _productRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure(DomainErrors.Product.ProductNotFound(request.Id));
        }

        _productRepository.Delete(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
