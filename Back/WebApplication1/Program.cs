using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using NaureBack.Models;
using NaureBack.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NaureContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("conexionNaure"))
);

//PROYECTO
//AÑADIDO para la clase de control de los token en el back
builder.Services.AddSingleton<PalabraClaveServicio>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
}
);

//PROYECTO
//AÑADIDO para las referencias cíclicas
//No hace nada ahora mismo con los DTOs
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
