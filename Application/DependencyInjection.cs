﻿using Application.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerLoanRateService, CustomerLoanRateService>();

        return services;
    }
}
