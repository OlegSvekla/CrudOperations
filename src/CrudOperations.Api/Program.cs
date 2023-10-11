using CrudOperations.Api.Extensions;
using CrudOperations.Domain.Entities;
using CrudOperations.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DbConfiguration.Configuration(builder.Configuration, builder.Services);
ServicesConfiguration.Configuration(builder.Services);
SwaggerConfiguration.Configuration(builder.Services);

var app = builder.Build();

await app.RunDbContextMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
