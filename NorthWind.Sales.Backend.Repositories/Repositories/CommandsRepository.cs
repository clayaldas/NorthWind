﻿using NorthWind.Sales.Backend.BusinessObjects.Aggregates;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Backend.Repositories.Interfaces;

// Para utilizar con Dapper
// using NorthWind.Sales.Backend.Repositories.Entities; 

// Librería nueva y recomendada para Dapper.
// using Microsoft.Data.SqlClient; 

namespace NorthWind.Sales.Backend.Repositories.Repositories;

//  ---------------------------------------------------------------
//  IMPLEMENTACIÓN DE UN REPOSITORY
//  ---------------------------------------------------------------
//  Cuando se necesita implementar una interface podemos hacernos las siguientes
//  preguntas y respuestas:
//
//  Pregunta:
//  1) Que se se desea implementar?:
//     Respuesta: la interfaz "ICommandsRepository".
//  Pregunta:
//  2) Que clase o clases o interfaces (dependencias) se necesita para esto?:
//     Respuesta: para implementar el repository se necesita "INorthWindSalesCommandsDataContext".

//  La interface "ICommandsRepository" pertenece a la capa (2. Application Business Rules - capa
//  Application).
//  La clase "CommandsRepository" pertenece a la capa (3. Interface Adapters - capa Infrastructure).
//  Estamos cumpliendo con la Regla de Dependencias: el dominio (Application) no depende de la
//  infraestructura (Interface Adapters), pero la infraestructura sí depende del dominio.
//  La clase "CommandsRepository" depende de una abstracción (INorthWindSalesCommandsDataContext)
//  y no de una implementación concreta de acceso a datos.
//
//  Aquí se está usando una característica de C# 12: el constructor implícito
//  (Primary Constructor) para inyectar la dependencia "INorthWindSalesCommandsDataContext".
//  Esto desacopla el repositorio del tipo concreto (implementación) de ORM que se vaya a
//  utilizar para realizar la persistencia en la la base de datos (por ejemplo, Entity
//  Framework, Dapper, etc.).
//
//  ---------------------------------------------------------------
//  NOTA:
//  ---------------------------------------------------------------
//  El repositorio no debe contener la "lógica de negocio", sino solo la
//  "lógica de acceso a datos". Esto mantiene separadas las responsabilidades, siguiendo
//  el principio SOLID Single Responsibility Principle (SRP).
//
//  Se utiliza el parámetro "context" para implementar los métodos.
//  Un "Repository"  es la clase principal que implementa la "lógica de persistencia"
//  de datos (comandos) relacionados con una orden.
internal class CommandsRepository(INorthWindSalesCommandsDataContext context) :
ICommandsRepository
{
  //  ** Método de que esta declarado en la interfaz: ICommandsRepository. ** 
  //  Representa una orden con sus detalles (patrón de DDD - Domain Driven Design).
  public async Task CreateOrder(OrderAggregate order)
  {
    //  Agregar la "cabecera" de la orden al repositorio.
    await context.AddOrderAsync(order);

    //  Agregar el detalle de la orden al repositorio.    
    await context.AddOrderDetailsAsync(
    //  Transformar los detalles de la orden (order.OrderDetails) que es un "agreggate"
    //  de la capa "2" se transforman en objetos de tipo Entities.OrderDetail antes de
    //  ser guardados.
    //  Esto asegura que los datos se adapten al formato esperado por el repositorio que es
    //  el que va a guardar los datos en la fuente de datos.
    order.OrderDetails
    .Select(d => new Entities.OrderDetail
    {
      Order = order,
      ProductId = d.ProductId,
      Quantity = d.Quantity,
      UnitPrice = d.UnitPrice
    }).ToArray());
  }

  //  ** Método que esta declarado en la interfaz: IUnitOfWork.  **  
  //  Se encarga de persistir los cambios como una sola unidad (transacción).
  public async Task SaveChanges() =>
  await context.SaveChangesAsync();
}

//  ----------------------------------------------------------
//  Principios aplicados:
//  ----------------------------------------------------------
//  1) DIP/DI — Dependency Inversion Principle (DIP): dependemos de abstracciones, no de
//               implementaciones concretas.
//  2) (IoC) —  Inversión de Control: control de la creación del contexto "context" no está
//               aquí, sino que se inyecta utilizando un contenedor IoC de Microsoft.

/*
#region Misma funcionalidad que utiliza Dapper y sin Entity Framework

internal class CommandsRepository : ICommandsRepository
{
  private readonly string _connectionString;
  //private readonly INorthWindSalesCommandsDataContext _context;
  public CommandsRepository(string connectionString)
  {
    _connectionString = connectionString;

  }

  public async Task CreateOrder(OrderAggregate order)
  {
    using (var connection = new SqlConnection(_connectionString))
    {
      await connection.OpenAsync();

      // 1️) Insertar la orden principal usando AddOrderAsync
      int orderId = await AddOrderAsync(connection, order);

      // 2️) Insertar los detalles de la orden usando AddOrderDetailsAsync
      await AddOrderDetailsAsync(connection, orderId, (List<OrderDetail>)order.OrderDetails);
    }
  }

  private async Task<int> AddOrderAsync(SqlConnection connection, OrderAggregate order)
  {
    string insertOrderSql = @"
            INSERT INTO Orders (CustomerId, OrderDate, ShipAddress)
            VALUES (@CustomerId, @OrderDate, @ShipAddress);
            SELECT CAST(SCOPE_IDENTITY() as int);";  // Obtener el ID generado

    using (var command = new SqlCommand(insertOrderSql, connection))
    {
      command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
      command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
      command.Parameters.AddWithValue("@ShipAddress", order.ShipAddress);

      // Ejecutar el comando y obtener el orderId
      var result = await command.ExecuteScalarAsync();
      return Convert.ToInt32(result); // Convertir el resultado a int
    }
  }

  private async Task AddOrderDetailsAsync(SqlConnection connection, int orderId, List<OrderDetail> orderDetails)
  {
    string insertOrderDetailSql = @"
            INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice)
            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice)";

    // Usamos SqlCommand para ejecutar el insert por cada detalle de la orden
    foreach (var detail in orderDetails)
    {
      using (var command = new SqlCommand(insertOrderDetailSql, connection))
      {
        command.Parameters.AddWithValue("@OrderId", orderId);
        command.Parameters.AddWithValue("@ProductId", detail.ProductId);
        command.Parameters.AddWithValue("@Quantity", detail.Quantity);
        command.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);

        await command.ExecuteNonQueryAsync(); // Ejecutamos el comando por cada detalle
      }
    }
  }

  public Task SaveChanges()
  {
    throw new NotImplementedException();
  }
}


#endregion
*/
