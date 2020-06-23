using Galchenko.TestTask.Domain;
using Galchenko.TestTask.Persistence.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Galchenko.TestTask.Persistence.Configurations
{
    public sealed class AddressConfiguration : IEntityTypeConfiguration< Address >
    {
        public void Configure( EntityTypeBuilder< Address > builder )
        {
            builder.ToTable( "Addresses", "dbo" );

            builder
                .Property< int >("Id")
                .HasColumnType( MssqlTypes.INT )
                .ValueGeneratedOnAdd();
            builder.HasKey( "Id" );

            builder.Property( a => a.Line1 ).HasColumnType( MssqlTypes.NVARCHAR( 512 ) ).IsRequired( true );
            builder.Property( a => a.Line2 ).HasColumnType( MssqlTypes.NVARCHAR( 512 ) ).IsRequired( false );
            builder.Property( a => a.City ).HasColumnType( MssqlTypes.NVARCHAR( 256 ) ).IsRequired( true );
            builder.Property( a => a.PostalCode ).HasColumnType( MssqlTypes.NVARCHAR( 16 ) ).IsRequired( true );
        }
    }
}
