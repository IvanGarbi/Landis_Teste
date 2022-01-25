using Landis_Teste.Business.Interfaces;
using Landis_Teste.Business.Notificacoes;
using Landis_Teste.Business.Services;
using Landis_Teste.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Landis_Teste.Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var _repository = serviceProvider.GetService<IEndpointRepository>();
            var _service = serviceProvider.GetService<IEndpointService>();
            var _notificador = serviceProvider.GetService<INotificador>();

            var menu = new Menu(_repository, _service, _notificador);

        }
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IEndpointRepository, EndpointRepository>();
            services.AddScoped<IEndpointService, EndpointService>();
            services.AddScoped<INotificador, Notificador>();
        }
    }
}
