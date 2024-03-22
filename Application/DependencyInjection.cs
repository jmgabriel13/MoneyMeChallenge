using Application.Customers;
using Application.LoanApplications;
using Application.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ILoanApplicationService, LoanApplicationService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}