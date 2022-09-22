using System.Net;
using BookingSystemAPI.Repositories;
using BookingSystemAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Reflection;
using BookingSystemAPI.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x =>
{
    x.ReturnHttpNotAcceptable = true;
    x.OutputFormatters.RemoveType<StringOutputFormatter>();
});

var executingAssembly = Assembly.GetExecutingAssembly().GetName().Name;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme"
    });

    x.AddSecurityRequirement(new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlCommentsFile = $"{executingAssembly}.xml";
    x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlCommentsFile));
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:Secret"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddFluentValidation();
builder.Services.AddFluentValidationRulesToSwagger();

builder.Services.AddHttpClient("HotelsEndpoint", x =>
{
    x.BaseAddress = new Uri(builder.Configuration["IntegrationEndpoints:HotelsUri"]);
});

builder.Services.AddHttpClient("FlightsEndpoint", x =>
{
    x.BaseAddress = new Uri(builder.Configuration["IntegrationEndpoints:FlightsUri"]);
});

builder.Services.AddSingleton<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IIntegrationService, HotelService>();
builder.Services.AddScoped<IIntegrationService, HotelAndFlightService>();
builder.Services.AddScoped<IntegrationServiceFactory>();
builder.Services.AddTransient<IManagerService, ManagerService>();

var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler(x =>
    {
        x.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptionHandlerFeature != null)
            {
                if (exceptionHandlerFeature.Error is not AppException applicationException)
                {
                    const string serverErrorMessage = "An unexpected error occurred.  Please try again later.";
                    context.Response.AddApplicationError(serverErrorMessage);

                    // log the error
                    app.Logger.LogError(context.Response.StatusCode, exceptionHandlerFeature.Error,
                        exceptionHandlerFeature.Error.Message);

                    await context.Response.WriteAsync(serverErrorMessage);
                    return;
                }

                context.Response.AddApplicationError(exceptionHandlerFeature.Error.Message, applicationException.IsJson);
                await context.Response.WriteAsync(exceptionHandlerFeature.Error.Message);
            }
        });
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization();

app.Run();
