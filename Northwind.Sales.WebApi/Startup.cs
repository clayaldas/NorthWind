//using Microsoft.AspNetCore.Mvc.ApiExplorer;

using NorthWind.Sales.Backend.DataContexts.EFCore.Options;
using NorthWind.Sales.Backend.IoC;

namespace Northwind.Sales.WebApi;

// Esto expone 2 métodos se extensión para configurar los servicios web 
// y agregar los middlewares y endpoints de la Web API
internal static class Startup
{
	// Método de extensión.
	public static WebApplication CreateWebApplication (this WebApplicationBuilder builder)
	{

		// Configurar APIExplorer para descrubir y exponer
		// los metadatos de los "endpoints" de la aplicacion.
		builder.Services.AddEndpointsApiExplorer();

		// Agregar el generador que construye los objetos de
		// documentación de Swagger que tenga la funcionalidad de: ApiExplorer
		builder.Services.AddSwaggerGen();

		// Rigistrar los servicios de la aplicacion que se van a exponer.
		builder.Services.AddNorthWindSalesServices(
			dbObtions=> builder.Configuration.GetSection(DBOptions.SectionKey).Bind(dbObtions));

		// Agregar el servicio CORS para clientes (Web, Movil, etc.) que se ejecuten
		// en el navegador Web (Blazor, React, Angular).
		builder.Services.AddCors(options => {
			options.AddDefaultPolicy(config =>
		{
			config.AllowAnyMethod();
			config.AllowAnyHeader();
			config.AllowAnyOrigin();
		});

		});

		return builder.Build();

	}

	public static WebApplication ConfigureWebApplication(this WebApplication app)
	{
		// Habilitar el middleware para que se muestra la información en formato Json
		// y la interfaz UI de Swagger lo puede visualizar en el desarrollo.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		// Registrar lso endpoints (url) de la aplicación.
		app.MapNorthWindSalesEndpoints();

		// Agregarf el middleware CORS
		app.UseCors();

		return app;
	}
}
