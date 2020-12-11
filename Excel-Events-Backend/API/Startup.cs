using System;
using API.Data;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Controllers
            services.AddControllers().AddNewtonsoftJson(AppDomainManagerInitializationOptions =>
                AppDomainManagerInitializationOptions.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Add Database to the Services
            services.AddDbContext<DataContext>(options =>
            {
                string connectionString = Environment.GetEnvironmentVariable("POSTGRES_DB");
                options.UseNpgsql(connectionString);
            });

            // Add Automapper to map objects of different types
            services.AddAutoMapper(opt => { opt.AddProfile(new AutoMapperProfiles()); });

            // Adding Swagger for Documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Excel Events", Version = "v 1.0"});
                c.DocumentFilter<SwaggerPathPrefix>(Environment.GetEnvironmentVariable("API_PREFIX"));
                c.EnableAnnotations();
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });

            // Adding Custom Services
            services.AddCustomServices();

            // Adding Repositories
            services.AddRepositoryServices();



            services.AddAuthentication("JwtAuthentication")
                .AddScheme<BasicAuthenticationOptions, CustomAuthenticationHandler>("JwtAuthentication", null);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ServiceAccount", policy => policy.RequireClaim("ServiceAccount"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Environment.GetEnvironmentVariable("API_PREFIX") + "/swagger/v1/swagger.json","Excel Events");
            });

            // Add CORS
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Middleware for Routing
            app.UseRouting();

            app.UseAuthentication();

            // Middleware for Authorization
            app.UseAuthorization();

            // Middleware for catching exceptions and return custom messages
            app.ConfigureExceptionHandlerMiddleware();

            // Middleware for Specifying Endpoints
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
