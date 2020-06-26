using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Galchenko.TestTask.ApplicationLayer;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.DesktopClient.DialogWindows;
using Galchenko.TestTask.Persistence;
using Galchenko.TestTask.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using p1eXu5.Wpf.MvvmBaseLibrary;

namespace Galchenko.TestTask.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IConfiguration _configuration = default!;

        private DialogRepository? _dialogRepository = null!;

        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );

            var builder = 
                new ConfigurationBuilder()
                    .SetBasePath( AppDomain.CurrentDomain.BaseDirectory )
                    .AddJsonFile( "appsettings.json", optional: false, reloadOnChange: true );

            _configuration = builder.Build();


            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient< IConfiguration >( sp => Configuration );
            ConfigureServices( serviceCollection, _configuration );

            var mainWindow = new MainWindow();
            _dialogRepository = RegisterDialogs( mainWindow );

            serviceCollection.AddSingleton< DialogRepository >( _ => _dialogRepository );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            
            
            SeedData();

            var mvm = ServiceProvider.GetRequiredService< MainViewModel >();
            mainWindow.DataContext = mvm;
            mainWindow.Show();
        }

        private void OnDispatcherUnhandledException( object sender, DispatcherUnhandledExceptionEventArgs e )
        {
            MessageBox.Show("An error has occurred and the application will now close.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
			//e.Handled = true;
        }


        public IServiceProvider ServiceProvider { get; private set; } = default!;

        public IConfiguration Configuration => _configuration;



        private static DialogRepository RegisterDialogs( Window wnd )
        {
            DialogRepository dialogRepository = new DialogRepository( wnd );
            dialogRepository.Register< PatientUpdateViewModel, PatientDialogWindow >();
            dialogRepository.Register< PatientNewViewModel, PatientDialogWindow >();
            dialogRepository.Register< AppointmentViewModel, AppointmentDialogWindow >();
            dialogRepository.Register< ErrorViewModel, ErrorDialogWindow >();
            return dialogRepository;
        }


        private void ConfigureServices( IServiceCollection services, IConfiguration configuration )
        {
            services.AddLogging();

            services.AddApplicationLayer();
            services.AddPersistence( configuration );
            services.AddViewModels();
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
