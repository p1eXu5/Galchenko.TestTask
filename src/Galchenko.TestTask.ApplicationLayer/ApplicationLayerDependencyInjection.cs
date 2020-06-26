using System.Linq;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using Galchenko.TestTask.ApplicationLayer.Patients.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Galchenko.TestTask.ApplicationLayer
{
    public static class ApplicationLayerDependencyInjection
    {
        public static void AddApplicationLayer( this IServiceCollection services )
        {
            // **********
            // * Mapper *
            // **********
            services.AddAutoMapper(
                 (sp, mcfg) => 
                     mcfg.AddProfile( new ApplicationProfile( sp ) ),
                 Enumerable.Empty< Assembly >()
            );


            services.AddValidatorsFromAssembly( typeof( PatientDtoBaseValidator ).Assembly );
            services.AddTransient< IRepository, Repository >();
        }
    }
}
