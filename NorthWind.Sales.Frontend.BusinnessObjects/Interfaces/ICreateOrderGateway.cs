//using NorthWind.Sales.Entities.Dtos.CreateOrder;

namespace NorthWind.Sales.Frontend.BusinnessObjects.Interfaces;

// Esta interface permitirá implementar un clase para encapusular el código cliente que
// consuma la Web API y crear la orden desde Blazor.
public interface ICreateOrderGateway
{
    Task<int> CreateOrderAsync(CreateOrderDto order);
}
