using ProjetoTesteAPI.Infrastructure.Interfaces;
using ProjetoTesteAPI.Infrastructure.Repositories;
using ProjetoTesteAPI.Infrastructure;
using ProjetoTesteAPI.Models;

namespace ProjetoTesteAPI.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using ProjetoTesteAPI.Controllers.Services;

    public static class AdditionExtension
    {
        public static IServiceCollection ConfigureAddition(this IServiceCollection services)
        {
            services.AddScoped<BrandService>();
            services.AddScoped<ClientService>();
            services.AddScoped<ProductService>();
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