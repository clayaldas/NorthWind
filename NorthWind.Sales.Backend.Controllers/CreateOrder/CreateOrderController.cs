﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Backend.BusinessObjects.POCOEntities;
using NorthWind.Sales.Entities.Dtos.CreateOrder;
using NorthWind.Sales.Entities.ValueObjects;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace NorthWind.Sales.Backend.Controllers.CreateOrder;

// CreateOrderController: Esta clase define un controller minimalista en forma de clase estática.
// Se usa esta forma para agrupar la configuración del endpoint sin depender de clases de tipo
// Controller tradicionales.
// Resumen
//  Este código:
//  Agrega un endpoint POST / "CreateOrder".
//  - Recibe una orden como DTO.
//  - Ejecuta un caso de uso desacoplado.
//  - Retorna el ID de la orden creada.
public static class CreateOrderController
{
  //  --------------------------------------------------------------------------------------
  //  Este método podrá ser consumido por un cliente remoto a través de la API Web.
  //  --------------------------------------------------------------------------------------
  //  Método de extensión que registra el endpoint HTTP POST para crear una orden.
  public static WebApplication UseCreateOrderController(this WebApplication app)
  {
    //  MapPost: registra una ruta y asocia la acción CreateOrder.
    //  Endpoints.CreateOrder: constante "/orders". Definida en la clase "Endpoints"
    //  capa "1. Enterprise Business Rules", carpeta: "ValueObjects".
    app.MapPost(Endpoints.CreateOrder, CreateOrder);

    // Se devuelve la instancia app para permitir encadenamiento fluido.
    return app;
  }

  //  --------------------------------------------------------------------------------------
  //  Este método podrá ser consumido por alguna aplicación de escritorio o backend, por
  //  ejemplo, una aplicación MVC.
  //  --------------------------------------------------------------------------------------
  //  CreateOrder: Este método maneja la lógica del endpoint HTTP POST y ejecuta el caso de uso asociado..
  //  CreateOrderDto orderDto: el DTO con los datos de la orden que se van a recibir en la petición.
  //  ICreateOrderInputPort: interfaz que representa el caso de uso o interactor en la capa de aplicación.
  //  ICreateOrderOutputPort: interfaz que representa el presentador, que formatea la respuesta.
  //                          Representa la salida del caso de uso.
  public static async Task<int> CreateOrder(
      CreateOrderDto orderDto,
      ICreateOrderInputPort inputPort,
      ICreateOrderOutputPort presenter
  )
  {
    //  Se llama al método Handle(orderDto) del InputPort, que contiene la lógica de negocio para crear la orden.
    //  Cuando el método Handle termina: El OutputPort (presenter) tiene el OrderId generado.
    await inputPort.Handle(orderDto);

    //  Se retorna el OrderId como respuesta desde el Presenter.
    return presenter.OrderId;
  }
}

//  --------------------------------------------------------------------------------------
//  NOTA:
//  --------------------------------------------------------------------------------------
//  Actualmente el método "CreateOrder" devuelve un int (OrderId) directamente.
//  Idealmente en una API real debería devolver un "ActionResult" o algo más detallado.
//  Por ejemplo: return Results.Created($"/orders/{presenter.OrderId}", presenter.OrderId);
//  Así se devuelve un mensaje "201 Created" más correcto según HTTP Semántica requerida.

//  --------------------------------------------------------------------------------------
//  ¿Qué es InputPort y OutputPort?
//  --------------------------------------------------------------------------------------
//  Esto sigue la terminología de Clean Architecture original de Uncle Bob:
//    InputPort = el Use Case (interactor). Implementa la lógica de negocio.
//    OutputPort = el Presenter. Devuelve la respuesta transformada desde la lógica.
//  Así, el controller no sabe cómo se crea la orden, ni cómo se formatea la respuesta. Solo delega:
//    inputPort.Handle(orderDto); → manda el DTO al caso de uso.
//    Luego espera a que presenter.OrderId tenga el resultado.
//
//  Este patrón desacopla totalmente el controller de la lógica y de la respuesta, cumpliendo con los
//  principios SOLID:
//  - SPR: El Controller tiene una sola responsabilidad.
//  - DIP: Se depende de interfaces (ICreateOrderInputPort, ICreateOrderOutputPort) no de clases.

//  Buenas prácticas:
//  - No hay lógica de negocio en el controller.
//  - Se usa inyección de dependencias por interfaz.
//  - Desacopla HTTP de la aplicación.
//  - Permite pruebas unitarias del Use Case sin depender de ASP.NET.
//  - Sigue Clean Architecture (capas independientes).
