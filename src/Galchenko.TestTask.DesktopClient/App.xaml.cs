using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Galchenko.TestTask.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IConfiguration _configuration;


        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );

            var builder = 
                new ConfigurationBuilder()
                    .SetBasePath( AppDomain.CurrentDomain.BaseDirectory )
                    .AddJsonFile( "appsettings.json", optional: false, reloadOnChange: true );

            _configuration = builder.Build();


            var serviceCollection = new ServiceCollection();
            ConfigureServices( serviceCollection, _configuration );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            SeedData();

            var wnd = new MainWindow();
            wnd.Show();
        }


        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration => _configuration;



        
        private void ConfigureServices( IServiceCollection services, IConfiguration configuration )
        {
            services.AddPersistence( configuration );
     
            services.AddTransient( typeof(MainWindow) );
        }

        private void SeedData()
        {
            var serviceProvider = ServiceProvider;

            using var scope = serviceProvider.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService< IApplicationDbContext >();

            DataSeeder.SeedData( dbContext );
        }
    }
}
