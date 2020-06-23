using Galchenko.TestTask.ApplicationLayer.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Galchenko.TestTask.Persistence
{
    public static class PersistenceDependencyInjection
    {
        public static IServiceCollection AddPersistence( this IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext< ApplicationDbContext >( options => {
                options.UseSqlServer(
                        configuration.GetConnectionString( "DefaultConnection" ),
                        b => b.MigrationsAssembly( typeof( ApplicationDbContext ).Assembly.FullName ) );
            });

            services.AddScoped< IApplicationDbContext >( provider => provider.GetService< ApplicationDbContext >() );

            return services;
        }
    }
}
