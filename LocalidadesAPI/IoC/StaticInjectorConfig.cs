using LocalidadesAPI.Interfaces.Repositories;
using LocalidadesAPI.Repositories;

namespace LocalidadesAPI.IoC
{
    public static class StaticInjectorConfig
    {
        public static void RegisterStaticDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IIBGERepository, IBGERepository>();
        }
    }
}
