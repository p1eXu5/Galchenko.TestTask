using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galchenko.TestTask.Domain;
using Galchenko.TestTask.Persistence.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Galchenko.TestTask.Persistence.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration< Appointment >
    {
        public void Configure( EntityTypeBuilder< Appointment > builder )
        {
            builder.ToTable( "Appointments", "dbo" );

            builder.HasKey( a => a.Id );
            builder.Property( a => a.Id ).HasColumnType( MssqlTypes.INT ).ValueGeneratedOnAdd();

            builder.Property( a => a.Date ).HasColumnType( MssqlTypes.DATE_TIME_OFFSET ).IsRequired();

            builder
                .Property( p => p.Type )
                .HasColumnType( MssqlTypes.NVARCHAR( 8 ) )
                .HasConversion(
                    v => v.ToString(),
                    v => ( AppointmentType )Enum.Parse( typeof( AppointmentType ), v ) );

            builder.Property( a => a.Diagnosis ).HasColumnType( MssqlTypes.NVARCHAR() ).IsRequired();

            builder.HasOne( a => a.Patient ).WithMany( p => p.Appointments ).HasForeignKey( a => a.PatientId );
        }
    }
}
