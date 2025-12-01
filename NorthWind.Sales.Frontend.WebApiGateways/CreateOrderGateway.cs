//using NorthWind.Sales.Entities.Dtos.CreateOrder;
//using NorthWind.Sales.Entities.ValueObjects;
//using NorthWind.Sales.Frontend.BusinnessObjects.Interfaces;
//using System.Net.Http.Json;

namespace NorthWind.Sales.Frontend.WebApiGateways;

internal class CreateOrderGateway(HttpClient client) : ICreateOrderGateway
{
    public async Task<int> CreateOrderAsync(CreateOrderDto order)
    {
        int OrderId = 0;

        var Response = await client.PostAsJsonAsync(
            EndPoints.CreateOrder, order);

        if (Response.IsSuccessStatusCode)
        {
            OrderId = await Response.Content.ReadFromJsonAsync<int>();
        }

        return OrderId;
    }
}
