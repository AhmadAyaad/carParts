using CarParts.Infrastructure.Repository;
using CarParts.Infrastructure.UnitOfWork;
using CarParts.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParts.Infrastructure
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IAuthRepository, AuthRepository>();
            //services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();

            services.AddScoped<ProductRepository>();
            services.AddScoped<IRepository<Product>>(p => p.GetRequiredService<ProductRepository>());
            services.AddScoped<IProductRepository>(x => x.GetRequiredService<ProductRepository>());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository    >();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
