﻿using Microsoft.Extensions.DependencyInjection;

namespace WorkerTrackingServer.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
        });

        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        return services;
    }
}
