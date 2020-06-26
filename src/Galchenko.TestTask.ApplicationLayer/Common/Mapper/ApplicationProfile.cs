using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Contracts;
using Galchenko.TestTask.ApplicationLayer.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#pragma warning disable 618

// ReSharper disable once IdentifierTypo
namespace Galchenko.TestTask.ApplicationLayer.Common.Mapper
{
    public class ApplicationProfile : Profile, IApplicationProfile
    {
        /// <summary>
        /// For testing purpose.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="passPhrase"></param>
        protected ApplicationProfile( ILogger< IApplicationProfile > logger, string passPhrase )
        {
            PassPhrase = passPhrase;
            Logger = logger;

            Setup();
            CreateCommonMaps();
        }

        public ApplicationProfile( IServiceProvider serviceProvider )
        {
            var configuration = serviceProvider.GetRequiredService< IConfiguration >();

            PassPhrase = configuration["Identity:PassPhrase"];
            Logger = serviceProvider.GetRequiredService< ILogger< IApplicationProfile > >();

            Setup();
            CreateCommonMaps();

            ApplyMappingsFromAssembly( Assembly.GetExecutingAssembly(), configuration );
        }

        private void Setup()
        {
            this.AllowNullCollections = true;
            this.AllowNullDestinationValues = true;
        }

        private void CreateCommonMaps()
        {
            this.CreateMap<Int64, DateTimeOffset>().ConvertUsing(s => DateTimeOffset.FromUnixTimeMilliseconds(s));
            this.CreateMap<DateTimeOffset, Int64>().ConvertUsing(dt => dt.ToUnixTimeMilliseconds());
            this.CreateMap<Int32, EntityIdDto>().ConvertUsing(i => i);
            this.CreateMap<EntityIdDto, Int32>().ConvertUsing(i => i);
        }


        public Profile Instance => this;
        public string PassPhrase { get; }
        public ILogger< IApplicationProfile > Logger { get; }

        /// <summary>
        /// <para>
        /// Searches <see cref="IMap"/>, <see cref="IDestinationMap{TS,TD}"/> and <see cref="ISourceMap{TS,TD}"/>
        /// inherit classes and invokes CreateMap method on they.
        /// <br/>
        /// If class inherit to <see cref="IReversedMap"/> <see cref="IMappingExpression.ReverseMap"/> calling.
        /// </para>
        /// <para>
        /// Class with CreateMap implementation can contains parameter constructor with pass phrase, name of this parameter must be "passPhrase".
        /// </para>
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="configuration"></param>
        protected void ApplyMappingsFromAssembly( Assembly assembly, IConfiguration configuration )
        {
            ApplyInterfaceMappings( assembly, configuration);
            ApplyAttributeMappings( assembly, configuration );
        }

        protected void ApplyInterfaceMappings( Assembly assembly, IConfiguration configuration )
        {
            var passPhrase = configuration["Identity:PassPhrase"];

            var ttt = assembly.GetExportedTypes().OrderBy( t => t.Name ).ToArray();
            var types = assembly.GetExportedTypes()
                .Where(t => 
                    t.GetInterfaces()
                        .Any(i => 
                                (i.IsGenericType && new[] {typeof(ISourceMap<,>), typeof( IDestinationMap<,>)}.Contains( i.GetGenericTypeDefinition() ) )
                                || (!i.IsGenericType && i == typeof(IMap) ) ) )
                .ToList();

            foreach (var type in types)
            {
                var instance = 
                    type.GetConstructors().Any( ci => ci.GetParameters().Any( pi => pi.ParameterType == typeof( string ) && pi.Name == nameof( passPhrase ) ) ) 
                        ? Activator.CreateInstance(type, passPhrase ) 
                        : Activator.CreateInstance(type);

                var interfaceType = type.GetInterface("ISourceMap`2") ?? type.GetInterface("IDestinationMap`2") ?? type.GetInterface("IMap");

                var methodInfo =
                    type.GetMethod( "CreateMap" )
                        ?? interfaceType?.GetMethod( "CreateMap" );


                //var genericMethodInfo = methodInfo?.MakeGenericMethod( type );

                var expr = methodInfo?.Invoke( instance, new object[] { this } );

                if ( expr != null
                     && type.GetInterface( nameof(IReversedMap) ) != null ) 
                {
                    var reverseMethodInfo
                        = type.GetMethod( nameof(IReversedMap.CreateReverseMap) )
                          ?? type.GetInterface( nameof(IReversedMap) )?.GetMethod( nameof(IReversedMap.CreateReverseMap) );

                    var arg1 = interfaceType!.GenericTypeArguments[0];
                    var arg2 = interfaceType!.GenericTypeArguments[1];
                    var genericReverseMethod = reverseMethodInfo?.MakeGenericMethod( arg1, arg2 );
                        //interfaceType == typeof( ISourceMap< , > )
                        //    ? reverseMethodInfo?.MakeGenericMethod( type, arg )
                        //    : reverseMethodInfo?.MakeGenericMethod( arg, type );

                    genericReverseMethod?.Invoke( instance, new object[] { expr } );
                }
            }
        }

        protected void ApplyAttributeMappings( Assembly assembly, IConfiguration configuration )
        {
            var types = assembly.GetExportedTypes().Where( t => t.GetCustomAttributes<MapAttribute>().Any() ).ToArray();
            foreach ( var type in types ) {
                CreateMaps( type );
            }
        }

        public void CreateMaps( Type type )
        {
            var attributes = type.GetCustomAttributes< MapAttribute >().ToArray();
            foreach ( var mapAttribute in attributes ) {
                mapAttribute.CreateMap( this, type );
            }
        }
    }
}
