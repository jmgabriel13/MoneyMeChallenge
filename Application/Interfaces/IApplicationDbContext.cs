using Domain.Entities.CalculatedQoutes;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;
public interface IApplicationDbContext
{
    DbSet<CalculatedQuote> CalculatedQuotes { get; set; }
}