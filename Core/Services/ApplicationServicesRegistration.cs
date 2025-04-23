

using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            // Register AutoMapper for object mapping
            Services.AddAutoMapper(typeof(Services.ServicesManager).Assembly);

            //ServiceManager
            Services.AddScoped<IServicesManager, ServicesManager>();

            return Services;

        }
    }
}
