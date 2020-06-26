using System;
using System.Collections.Generic;
using AutoMapper;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Galchenko.TestTask.ApplicationLayer.Tests.MapConfigurationTests
{
    public abstract class MapConfigurationTestsBase
    {
        public MapperConfiguration MapperConfiguration { get; private set; } = default!;
        public IMapper Mapper { get; private set; } = default!;
        public string PassPhrase { get; } = "test-phrase";

        public ILogger< IApplicationProfile > StubLogger { get; set; } =
            new Mock< ILogger< IApplicationProfile > >().Object;

        [SetUp]
        public void SetupConfiguration()
        {
            MapperConfiguration = new MapperConfiguration( cfg => {
                cfg.AddProfile( GetProfile() );
            } );


            MapperConfiguration.AssertConfigurationIsValid();

            Mapper = new Mapper( MapperConfiguration );
        }


        #region factories

        protected abstract IEnumerable< Type > MappedTypes { get; }

        protected virtual Profile GetProfile()
        {
            var profile = new TestProfile( StubLogger, PassPhrase );

            CreateTestMaps( profile );

            return profile;
        }

        protected void CreateTestMaps( ApplicationProfile profile )
        {
            foreach ( var mappedType in MappedTypes ) {
                profile.CreateMaps( mappedType );
            }
        }

        #endregion


        #region fakes
        private class TestProfile : ApplicationProfile
        {
            public TestProfile( ILogger< IApplicationProfile > logger, string passPhrase )
                : base( logger, passPhrase )
            { }
        }

        #endregion
    }
}
