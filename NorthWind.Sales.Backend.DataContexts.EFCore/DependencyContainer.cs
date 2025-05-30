﻿using Microsoft.Extensions.DependencyInjection; //IServiceCollection
using NorthWind.Sales.Backend.DataContexts.EFCore.Options;//DBOptions
using NorthWind.Sales.Backend.DataContexts.EFCore.Services;//NorthWindSalesCommandsDataContext
using NorthWind.Sales.Backend.Repositories.Interfaces;//INorthWindSalesCommandsDataContext

namespace NorthWind.Sales.Backend.DataContexts.EFCore;

//  Este código implementa un contenedor de inyección de dependencias (IoC container registration)
//  en el contexto de Clean Architecture, en la capa de Frameworks & Drivers, usando el motor
//  de inyección de dependencias de .NET (Microsoft.Extensions.DependencyInjection).
public static class DependencyContainer
{
  //  Es un método de extensión sobre IServiceCollection.
  //  Recibe una acción para configurar el objeto "DBOptions", el cual contiene la cadena de conexión de base de datos.
  public static IServiceCollection AddDataContexts(this IServiceCollection services,
      Action<DBOptions> configureDBOptions
  )
  {
    //  Registra las opciones de configuración (DBOptions) para que se puedan inyectar con IOptions<DBOptions>.
    //  Generalmente esto se usa junto con el archivo "appsettings.json".
    services.Configure(configureDBOptions);
    //  Registra una implementación con alcance Scoped (1 por solicitud/request web).
    //  Se inyectará la clase "NorthWindSalesCommandsDataContext" cuando se requiera la interfaz
    //  "INorthWindSalesCommandsDataContext".
    //  Se respeta la inversión de dependencias (DIP): la aplicación depende de una abstracción, no de una
    //  implementación concreta.
    services.AddScoped<INorthWindSalesCommandsDataContext, NorthWindSalesCommandsDataContext>();
    //  Devuelve la colección "services" modificada, lo que permite usar con fluidez en el archivo Program.cs.
    return services;
  }
}

//  ---------------------------------------------------
//  ¿Qué se logra con esto?
//  ---------------------------------------------------
//  - Registrar correctamente esto en el archivo Program.cs.
//  - Mostrar cómo se usa "INorthWindSalesCommandsDataContext" en un caso de uso.
//  - Integrarlo con la capa de Application para un flujo completo.