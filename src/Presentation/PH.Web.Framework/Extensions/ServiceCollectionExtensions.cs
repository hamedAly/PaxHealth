﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PH.Core.Data;
using PH.Data;
using PH.Services;
using PH.Data.Repo;
using PH.Web.Framework.Models;

namespace PH.Web.Framework
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            
            var dataSettings = builder.Configuration.GetSection("DataSettings").Get<DataSettings>();
            if (dataSettings == null)
            {
                throw new InvalidOperationException("DataSettings section is missing from configuration.");
            }

            // data layer configuration based on the provider
            services.AddDbContext<PHealthDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("DefaultConnection string is missing in configuration.");
                }

                if (dataSettings.Provider == "SqlServer")
                {
                    options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
                }
                else
                {
                    throw new NotSupportedException($"The database provider '{dataSettings.Provider}' is not supported.");
                }
            });

            // Register the DbContext as scoped
            services.AddScoped<IDbContext, PHealthDbContext>();

            // repository pattern registration
            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));

            // services registration
            services.AddTransient<ILabResultService, LabResultService>();
            services.AddTransient<IMedicalDoucmentService, MedicalDocumentService>();
        }
    }
}
