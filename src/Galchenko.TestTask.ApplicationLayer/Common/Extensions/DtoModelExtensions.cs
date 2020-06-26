using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Galchenko.TestTask.ApplicationLayer.Common.Extensions
{
    public static class DtoModelExtensions
    {
        public const string DTO_SUFFIX = "Dto";
        public const string ID_SUFFIX = "Id";
        public static string TrimDtoSuffix( this string dtoModelName )
        {
            var ind = dtoModelName.IndexOf( DTO_SUFFIX, StringComparison.Ordinal );

            return ind > 0
                ? dtoModelName.Substring( 0, ind )
                : dtoModelName;

        }

        public static string SplitByCamelCase( this string name )
        {
            if ( name.Length <= 1 ) return name;

            var sb = new StringBuilder( name.Length );
            sb.Append( name[0] );

            for ( int i = 1; i < name.Length; ++i ) {

                if ( Char.IsUpper( name[i] ) ) {
                    sb.Append( ' ' ).Append( Char.ToLower( name[i] ) );
                }
                else {
                    sb.Append( name[i] );
                }
            }

            return sb.ToString();
        }

        public static string SplitId( this string property )
        {
            var ind = property.IndexOf( ID_SUFFIX, StringComparison.Ordinal );

            return ind > 0
                ? $"{property.Substring( 0, ind )} {ID_SUFFIX}"
                : property;

        }

        public static string ToMessage( this string property )
            => property.SplitId().ToLowerCamelCase();

        public static string ToLowerCamelCase( this string property )
        {
            if ( property.Length == 0 ) throw new ArgumentException( $"{nameof(property)} must be not empty." );
            var result = Char.ToLower( property[0] ) + property.Substring( 1 );
            return result;
        }


        [ SuppressMessage( "ReSharper", "PossibleMultipleEnumeration" ) ]
        public static string ToArrayString< TKey >( this IEnumerable< IEntityIdDto< TKey > > source, string accumulator = "" )
        {
            if ( !source.Any() ) return String.Empty;

            var sb = new StringBuilder("[");
            if ( !String.IsNullOrWhiteSpace( accumulator ) ) {
                sb.Append( accumulator );
                sb.Append( ", " );
            }

            return source.Select( id => id.Id ).Aggregate( sb, ( acc, el) => acc.Append( $"{el}, " ) ).ToString()[..^2] + "]";
        }

        [ SuppressMessage( "ReSharper", "PossibleMultipleEnumeration" ) ]
        public static string ToArrayString< TKey >( this IEnumerable< TKey > source, string accumulator = "" )
            where TKey : notnull
        {
            if ( !source.Any() ) return String.Empty;

            var sb = new StringBuilder("[");
            if ( !String.IsNullOrWhiteSpace( accumulator ) ) {
                sb.Append( "\"" );
                sb.Append( accumulator );
                sb.Append( "\", " );
            }

            return source.Aggregate( sb, ( acc, el) => acc.Append( $"\"{el.ToString()}\", " ) ).ToString()[..^2] + "]";
        }
    }
}
