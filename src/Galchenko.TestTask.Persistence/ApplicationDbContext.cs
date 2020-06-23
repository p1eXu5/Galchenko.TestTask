using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.Domain;
using Galchenko.TestTask.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Galchenko.TestTask.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext( DbContextOptions options )
            : base(options)
        { }

        public DbSet< Appointment > Appointments { get; set; } = default!;
        public DbSet< Patient > Patients { get; set; } = default!;


        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating( builder );
            ApplyConfigurations( builder );
        }

        protected virtual void ApplyConfigurations( ModelBuilder builder )
        {
            builder.ApplyConfigurationsFromAssembly( typeof( PatientConfiguration ).Assembly );
        }
    }
}
