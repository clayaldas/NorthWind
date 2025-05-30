﻿using Microsoft.Extensions.Options;
using NorthWind.Sales.Backend.BusinessObjects.POCOEntities;
using NorthWind.Sales.Backend.DataContexts.EFCore.DataContexts;
using NorthWind.Sales.Backend.DataContexts.EFCore.Options;
using NorthWind.Sales.Backend.Repositories.Interfaces;

namespace NorthWind.Sales.Backend.DataContexts.EFCore.Services;

//  Este código define una clase llamada NorthWindSalesCommandsDataContext, que implementa
//  una unidad de trabajo para comandos de escritura para implementar el patrón Unit Of Work
//  sobre la base de datos, dentro de Clean Architecture, en la capa de Frameworks
//  and Drivers, usando Entity Framework Core

//  Hereda de NorthWindSalesContext, que ya tiene toda la lógica de conexión y configuración de EF Core.
//  Implementa la interfaz "INorthWindSalesCommandsDataContext" que es el repositorio de comandos el cual
//  se define en la capa (3. Interface Adapters-NorthWind.Sales.Backend.Repositories-Repositories).
internal class NorthWindSalesCommandsDataContext(IOptions<DBOptions> dbOptions)
    : NorthWindSalesContext(dbOptions), INorthWindSalesCommandsDataContext
{
  //  Agrega un objeto "Order" al contexto para ser persistido en la BD.
  public async Task AddOrderAsync(Order order) => await AddAsync(order);

  //  Agrega múltiples "OrderDetail" al contexto.
  public async Task AddOrderDetailsAsync(
      IEnumerable<Repositories.Entities.OrderDetail> orderDetails) => await AddRangeAsync(orderDetails);

  //  Persiste todos los cambios en la base de datos en una sola transacción (unidad de trabajo).
  public async Task SaveChangesAsync() => await base.SaveChangesAsync();
}

//  ----------------------------------------------------------------------------------------
//  ¿Dónde encaja esto en Clean Architecture?
//  ----------------------------------------------------------------------------------------
//  Capa: Entities (Domain). Rol: Define "Order" y "OrderDetail" como entidades o POCOs.
//  Capa: Use Cases/Application. Rol: Usa la interfaz "INorthWindSalesCommandsDataContext" para escribir.
//  Capa: Interface Adapters (Infrastructure). Rol: Implementa lógica de datos concreta que llama a esta clase.
//  Capa: Frameworks & Drivers (Presentation). Rol: Aquí se encuentra esta clase, que conecta EF Core con la aplicación.

//  ----------------------------------------------------------------------------------------
//  Ventajas del diseño
//  ----------------------------------------------------------------------------------------
//  Desacoplado: la aplicación depende de la interfaces, no de EF Core.
//  Testeable: se puede sustituir esta clase por un mock/fake para las pruebas.
//  Limpio: cumple con principios SOLID, especialmente SRP y DIP.
//  Persistencia clara: separa comandos(escritura) de queries(lectura), lo cual facilita
//                      la implementación del patron "CQRS".