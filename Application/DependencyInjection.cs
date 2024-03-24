using Application.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(assembly);
        });

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}