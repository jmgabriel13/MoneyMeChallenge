using Application.Interfaces;

namespace Application.Products.Delete;
public record DeleteProductCommand(Guid Id) : ICommand;
