using Application.Cronjob;
using Application.Interfaces;
using Application.Services;
using FluentValidation.AspNetCore;
using Infrastructures.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using WebAPI.Filters;
using WebAPI.Middlewares;
using WebAPI.Services;

namespace WebAPI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services,
            string userApp,
            string userAppDev,
            string key,
            string issuer,
            string audience)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            });
            services.AddEndpointsApiExplorer();
            // services.AddHealthChecks();
            // services.AddHealthChecks().AddCheck<ApiHealthCheck>(
            //     "TrainingManagementSystem",
            //     tags: new string[] { "TrainingManagementSystem" });
            // Middleware
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();
            // Extension Services
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<IFileService, FileService>();
            // Memory cache extension
            services.AddCache();



            services.AddHttpContextAccessor();
            // IValidator
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();



            #region Swagger
            services.AddSwaggerGen();
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
            #endregion
            #region  JWT configuration
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            #endregion

            #region Compression
            // compress file size response from server to client
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "application/json",
                    "application/xml",
                    "text/plain",
                    "image/png",
                    "image/jpeg"
                });
                options.Providers.Add<BrotliCompressionProvider>();
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            #endregion
            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyCors",
                policy =>
                {
                    policy.AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowAnyOrigin();
                        //.WithOrigins(new string[] { userApp });
                    // policy.WithOrigins(userApp, userApp)
                    //                  .AllowAnyHeader()
                    //                  .AllowAnyMethod();
                });
            });
            // Quartz
            services.AddTransient<SendAttendanceMailJob>();
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.ScheduleJob<SendAttendanceMailJob>(job => job
                    .WithIdentity("AttendanceMailJob")
                    .WithDescription("Sends attendance report emails at 11:10pm every day.")
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(11, 10))
                );
            });

            return services;
        }
    }
}
