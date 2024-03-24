using Application.Interfaces;

namespace Application.Products.Delete;
public sealed record DeleteProductCommand(Guid Id) : ICommand;
