using System;
using System.Collections.Generic;
using AutoMapper;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Galchenko.TestTask.ApplicationLayer.Tests.MapConfigurationTests
{
    [TestFixture]
    public class MapConfigurationTests : MapConfigurationTestsBase
    {
        [Test]
        public void Map_Should_HaveValidConfig()
        {
            MapperConfiguration.AssertConfigurationIsValid();
        }


        protected override IEnumerable< Type > MappedTypes { get; } = new Type[0];

        protected override Profile GetProfile()
        {
            var mockIConfiguration = new Mock< IConfiguration >();
            mockIConfiguration.Setup( c => c["Identity:PassPhrase"] ).Returns( "TestPassPhrase" );
            var mockILogger = new Mock< ILogger< IApplicationProfile > >();

            var mockService = new Mock< IServiceProvider >();
            mockService.Setup( s => s.GetService( typeof(IConfiguration) ) ).Returns( mockIConfiguration.Object );
            mockService.Setup( s => s.GetService( typeof(ILogger<IApplicationProfile> )) ).Returns( mockILogger.Object );

            var profile = new ApplicationProfile( mockService.Object );
            CreateTestMaps( profile );
            return profile;
        }
    }
}
