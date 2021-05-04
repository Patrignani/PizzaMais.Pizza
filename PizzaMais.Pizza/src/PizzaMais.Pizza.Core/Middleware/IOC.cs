using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using PizzaMais.Pizza.Communs.Interfaces;
using PizzaMais.Pizza.Communs.Interfaces.Service;
using PizzaMais.Pizza.Core.Service;

namespace PizzaMais.Pizza.Core.Middleware
{
    public static class IOC
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(_ => new NpgsqlConnection(configuration.GetConnectionString("PizzaMais")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IIngredienteService, IngredienteService > ();
            services.AddScoped<IUnidadeMedidaService, UnidadeMedidaService>();
            services.AddScoped<IBordaService, BordaService>();

            return services;
        }
    }
}
