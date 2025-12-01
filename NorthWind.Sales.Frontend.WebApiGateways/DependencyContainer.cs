//using Microsoft.Extensions.DependencyInjection;

namespace NorthWind.Sales.Frontend.WebApiGateways;

public static class DependencyContainer
{
    public static IServiceCollection AddWebApiGateWays(this IServiceCollection services, 
        Action<HttpClient> configureHttpClient)
    {
        services.AddHttpClient<ICreateOrderGateway, CreateOrderGateway>(configureHttpClient);

        return services;
    }
}
