using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ABCStore.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly))
                .AddAutoMapper(assembly);

        }
    }
}
