// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Api
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 02-02-2024 
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 06-25-2024
//  ****************************************************************
//  <copyright file="Program.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Api.Extensions;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Options;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Options;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Configuracion de Opciones
builder.Services
    .AddOptions<ApplicationOptions>()
    .Bind(builder.Configuration.GetSection(ApplicationOptions.Key))
    .Validate(option => option.DefaultPageSize > 0, "Page Size must be greater than 0.")
    .ValidateOnStart();
builder.Services.AddOptions<CacheOptions>()
    .Bind(builder.Configuration.GetSection(CacheOptions.Key));
#endregion

#region Services Register
builder.Services.AddControllers();
builder.Services.RegisterMiddleware();
builder.Services.RegisterApiVersion();
builder.Services.RegisterCORS(builder.Configuration);
builder.Services.RegisterSwagger();
builder.Services.RegisterDB();
builder.Services.RegisterDependency(builder.Configuration);
builder.Services.RegisterLogging(builder.Configuration);
builder.Services.RegisterHealthCheck(builder.Configuration);
#endregion

WebApplication app = builder.Build();

app.UseCors("AllowSpecificOrigin");

//HealthCheck Middleware4
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(delegate (Options options)
{
    options.UIPath = "/health-ui";
    options.AddCustomStylesheet("./Middleware/HealthCheck/custom.css");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json")
        .UseSwaggerUI(c =>
        {
            c.DocumentTitle = "CAFEMACA.Coink.PruebaTecnica API";
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CAFEMACA.Coink.PruebaTecnica API v1.0");

            //Add the new version
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "CAFEMACA.Coink.PruebaTecnica API v2.0");

            c.DisplayRequestDuration();
        });
    //app.UseDeveloperExceptionPage();
    app.UseExceptionHandler((_ => { })); // Should be always in first place 

}
else
{
    app.UseExceptionHandler((_ => { })); // Should be always in first place 
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseRouting().UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSerilogRequestLogging();

app.Run();
