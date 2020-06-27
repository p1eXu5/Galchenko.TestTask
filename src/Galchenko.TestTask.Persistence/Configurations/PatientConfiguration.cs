using System;
using Galchenko.TestTask.Domain;
using Galchenko.TestTask.Domain.Enums;
using Galchenko.TestTask.Persistence.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Galchenko.TestTask.Persistence.Configurations
{
    public sealed class PatientConfiguration : IEntityTypeConfiguration< Patient >
    {
        public void Configure( EntityTypeBuilder< Patient > builder )
        {
            builder.ToTable( "Patients", "dbo" );

            builder.HasKey( p => p.Id );
            builder.Property( p => p.Id ).HasColumnType( MssqlTypes.NCHAR(36) ).IsRequired();

            builder.Property( p => p.FirstName ).HasColumnType( MssqlTypes.NVARCHAR( 512 ) ).IsRequired();
            builder.Property( p => p.LastName ).HasColumnType( MssqlTypes.NVARCHAR( 512 ) ).IsRequired();
            builder.Property( p => p.MiddleName ).HasColumnType( MssqlTypes.NVARCHAR( 512 ) ).IsRequired( false );
            builder.Property( p => p.Phone ).HasColumnType( MssqlTypes.NVARCHAR( 32 ) ).IsRequired();

            builder
                .Property( p => p.Gender )
                .HasColumnType( MssqlTypes.NVARCHAR( 16 ) )
                .HasConversion(
                    v => v.ToString(),
                    v => ( Gender )Enum.Parse( typeof( Gender ), v ) );

            builder.Property( p => p.DateOfBirth ).HasColumnType( MssqlTypes.DATE ).IsRequired();

            builder.HasOne( p => p.Address ).WithOne().HasForeignKey< Patient >();
            builder.HasMany( p => p.Appointments ).WithOne( a => a.Patient );
        }
    }
}
