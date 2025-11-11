using DddCqrs.Application;
using DddCqrs.Infrastructure;
using DddCqrs.Infrastructure.Data;
using DddCqrs.Infrastructure.Data.DbContexts;
using DddCqrs.Presentation.Endpoints;
using DddCqrs.Presentation.Extensions;
using DddCqrs.Presentation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication()
    .AddInfrastructure(builder.Configuration);



builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

WebApplication app = builder.Build();

app.ApplyMigrations();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = "swagger"; 
});
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationWriteDbContext>();
    await DbSeeder.SeedAsync(context, services, CancellationToken.None);

}



app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.MapUserEndpoints();

app.Run();
