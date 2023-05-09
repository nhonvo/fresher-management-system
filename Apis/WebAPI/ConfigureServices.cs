using Application;
using Application.Interfaces;
using FluentValidation.AspNetCore;
using Infrastructures;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;
using WebAPI.Filters;
using WebAPI.Middlewares;
using WebAPI.Services;

namespace WebAPI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddHttpContextAccessor();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddSwaggerGen(options =>
         {
             options.SwaggerDoc("v1", new OpenApiInfo
             {
                 Title = "Training management system",
                 Version = "v1",
                 Description = "API for mooc project",
                 Contact = new OpenApiContact
                 {
                     Url = new Uri("https://google.com")
                 }
             });

             // Add JWT authentication support in Swagger
             var securityScheme = new OpenApiSecurityScheme

             {
                 Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                 Name = "Authorization",
                 In = ParameterLocation.Header,
                 Type = SecuritySchemeType.Http,
                 Scheme = "bearer",
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 }
             };

             options.AddSecurityDefinition("Bearer", securityScheme);

             var securityRequirement = new OpenApiSecurityRequirement
             {
                    {
                        securityScheme, new[] { "Bearer" }
                    }
             };

             options.AddSecurityRequirement(securityRequirement);
         });
            return services;
        }
    }
}
