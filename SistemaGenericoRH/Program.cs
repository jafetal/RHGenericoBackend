using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemaGenericoRH.Helpers;
using SistemaGenericoRH.Models;
using SistemaGenericoRH.Repositorys;
using SistemaGenericoRH.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services. to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ModelContext>(options =>
    {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
 );

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddScoped<SimpleAES>();

builder.Services.Scan(scan => scan
    .FromCallingAssembly().AddClasses(c => c.InNamespaces("SistemaGenericoRH.Repositorys")).AsImplementedInterfaces().WithTransientLifetime()
);

builder.Services.Scan(scan => scan
    .FromCallingAssembly().AddClasses(c => c.InNamespaces("SistemaGenericoRH.Services")).AsSelf().WithTransientLifetime()
);

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .WithMethods("GET", "POST", "PUT", "DELETE");
});

app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            var exceptionType = error.GetType();

            var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is GenericException)
            {
                context.Response.StatusCode =  400;
                await context.Response.WriteAsync(((GenericException)error.Error).ErrorMessage);
            }
            else
            {
                string errorMessageDefault = "Ocurrió un error inesperado, favor de contactar al administrador del sistema";
                context.Response.Headers.Add("Application-Error", errorMessageDefault);
                await context.Response.WriteAsync(errorMessageDefault);
            }
        }
    });
});

app.Run();
