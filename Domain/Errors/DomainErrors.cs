using Domain.Shared;

namespace Domain.Errors;
public static class DomainErrors
{
    public static class Product
    {
        public static Error ProductNotFound(Guid id) => new("Product.NotFound", $"The product with the ID = {id} was not found");
    }
}
