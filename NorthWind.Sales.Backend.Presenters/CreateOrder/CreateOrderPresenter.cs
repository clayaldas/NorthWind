using NorthWind.Sales.Backend.BusinessObjects.Aggregates;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.CreateOrder;

namespace NorthWind.Sales.Backend.Presenters.CreateOrder;

// La función del Presenter es la de convertir los datos del formato
// más conveniente (requerido) por los casos de uso (Use Cases)
// y entidades (Entities), al formato más conveniente (requerido)
// por algun agente externo como por ejempo la base de datos o una pagina web.
internal class CreateOrderPresenter : ICreateOrderOuputPort
{
    // regresar el Id de la orden
    public int OrderId { get; private set; }

    public Task Handle(OrderAggregate addedOrder)
    {
        // propiedad de escritura (private
        OrderId = addedOrder.Id;

        // tarea completada
        return Task.CompletedTask;
    }
}
