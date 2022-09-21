using BookingSystemAPI.Repositories;
using BookingSystemAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x =>
{
    x.ReturnHttpNotAcceptable = true;
    x.OutputFormatters.RemoveType<StringOutputFormatter>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddFluentValidation();
builder.Services.AddFluentValidationRulesToSwagger();

builder.Services.AddHttpClient("HotelsEndpoint", x =>
{
    x.BaseAddress = new Uri("https://tripx-test-functions.azurewebsites.net/api/SearchHotels");
});

builder.Services.AddHttpClient("FlightsEndpoint", x =>
{
    x.BaseAddress = new Uri("https://tripx-test-functions.azurewebsites.net/api/SearchFlights");
});

builder.Services.AddSingleton<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IIntegrationService, HotelService>();
builder.Services.AddScoped<IIntegrationService, HotelAndFlightService>();
builder.Services.AddScoped<IntegrationServiceFactory>();
builder.Services.AddTransient<IManagerService, ManagerService>();

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

app.Run();
