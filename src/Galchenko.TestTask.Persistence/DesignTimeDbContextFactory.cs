using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;


namespace Galchenko.TestTask.Persistence
{
    /// <summary>
    /// Use commands from Galchenko.TestTask.Persistence directory:
    /// dotnet ef migrations add  InitialCreate -s ..\Galchenko.TestTask.DesktopClient\Galchenko.TestTask.DesktopClient.csproj -p .\Galchenko.TestTask.Persistence.csproj
    /// dotnet ef database update  InitialCreate -s ..\Galchenko.TestTask.DesktopClient\Galchenko.TestTask.DesktopClient.csproj -p .\Galchenko.TestTask.Persistence.csproj
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory< ApplicationDbContext >
    {
        private const string ConnectionStringName = "DefaultConnection";

        public ApplicationDbContext CreateDbContext( string[] args )
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory!;
            return Create( basePath );
        }

        protected virtual ApplicationDbContext CreateNewInstance( DbContextOptions< ApplicationDbContext > options ) => new ApplicationDbContext( options );

        private ApplicationDbContext Create( string basePath )
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString( ConnectionStringName );

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactory.Create(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder< ApplicationDbContext >();

            // for set environment in Package Manager Console: $env:ASPNETCORE_ENVIRONMENT='<env_name>' 

            optionsBuilder.UseSqlServer( connectionString );
            optionsBuilder.EnableSensitiveDataLogging();

            return CreateNewInstance( optionsBuilder.Options );
        }
    }
}
