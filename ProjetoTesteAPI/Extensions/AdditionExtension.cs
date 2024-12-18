using ProjetoTesteAPI.Infrastructure.Interfaces;
using ProjetoTesteAPI.Infrastructure.Repositories;
using ProjetoTesteAPI.Infrastructure;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class AdditionExtension
    {
        public static IServiceCollection ConfigureAddition(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Brand>, BrandRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Client>, ClientRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped (typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services; 
        }
    }
}
