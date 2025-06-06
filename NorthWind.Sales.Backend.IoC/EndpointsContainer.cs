﻿using Microsoft.AspNetCore.Builder;
using NorthWind.Sales.Backend.Controllers.CreateOrder;

namespace NorthWind.Sales.Backend.IoC;

//  Clase estática usada como contenedor de dependencias/servicios de los endpoints
//  HTTP que expone la aplicación.
public static class EndpointsContainer
{
  //  Método de extensión para que desde Program.cs se pueda registrar fácilmente
  //  todos los endpoints de la capa externa, de forma limpia y modular: app.MapNorthWindSalesEndpoints();
  public static WebApplication MapNorthWindSalesEndpoints(this WebApplication app)
  {
    //  Este método está definido en "CreateOrderController" y se encarga de mapear un endpoint
    //  de tipo POST en la API para crear órdenes, como por ejemplo: app.MapPost("/orders", CreateOrder);
    //  Es parte de los adaptadores de entrada, y llama internamente al InputPort y OutputPort (Clean Architecture).
    app.UseCreateOrderController();

    return app;
  }
}
//  ----------------------------------------------------
//  ¿Cómo y donse se usaría esto?
//  ----------------------------------------------------
//  En el archivo principal de arranque (Program.cs):
//  Por ejemplo:  
//  var builder = WebApplication.CreateBuilder(args);
//  var app = builder.Build();
//  app.MapNorthWindSalesEndpoints(); // Aquí se usa la extensión.
//  app.Run();
//
//  ----------------------------------------------------
//   Ventajas de este enfoque
//  ----------------------------------------------------
//  - Modularidad: puedes tener varios controladores mapeados sin ensuciar el Program.cs.
//  - Clean Architecture compliance: separa responsabilidades claramente.
//  - Facilidad de testing y mantenimiento.