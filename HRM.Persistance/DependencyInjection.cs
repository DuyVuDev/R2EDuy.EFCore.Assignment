﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRM.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<HRMDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
                1024);
            return services;
        }
    }
}
