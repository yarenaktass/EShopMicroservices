﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            //// Add services to the container.
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddSingleton(connectionString);

            return services;
        }
    }
}
