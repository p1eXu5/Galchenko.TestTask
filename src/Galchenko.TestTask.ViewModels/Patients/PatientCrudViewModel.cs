using System;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Addresses.Dtos;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using Galchenko.TestTask.Domain;
using Galchenko.TestTask.ViewModels.Contracts;
using Microsoft.Extensions.Logging;
using p1eXu5.Wpf.MvvmBaseLibrary;

namespace Galchenko.TestTask.ViewModels.Patients
{
    public class PatientCrudViewModel : CrudViewModel< PatientRowViewModel, PatientNewDto, PatientUpdateDto, Patient, string >
    {
        public PatientCrudViewModel( IRepository repository, 
                                     DialogRepository dialogRepository,
                                     ILogger< PatientCrudViewModel > logger, 
                                     IMapper mapper,
                                     IValidator< PatientNewDto>patientPatientNewDtoValidator,
                                     IValidator< PatientUpdateDto > patientPatientUpdateDtoValidator
                                     ) 
            : base( repository, dialogRepository, logger, mapper )
        {
            PatientUpdateDtoValidator = patientPatientUpdateDtoValidator;
            PatientNewDtoValidator = patientPatientNewDtoValidator;
        }

        protected override Func< IQueryable< Patient >, IQueryable< Patient > >? LoadInclude { get; }
            = ApplicationLayer.Common.Repository.PatientIncludeAddress;


        protected IValidator< PatientNewDto > PatientNewDtoValidator { get; }
        protected IValidator< PatientUpdateDto > PatientUpdateDtoValidator  { get; }



        protected override (bool, PatientNewDto) CreateNewDto()
        {
            var newPatient = new PatientNewDto { Address = new AddressNewDto() };

            var vm = new PatientNewViewModel( newPatient, PatientNewDtoValidator, DialogRepository );
            var dialog = DialogRepository.GetView( vm );
            
            if (dialog?.ShowDialog() == true) {
                return (true, vm.Patient);
            }

            return (false, vm.Patient);
        }

        protected override (bool, PatientUpdateDto) UpdateDto< TDto >( TDto dto )
        {
            if ( !(dto is PatientUpdateDto patientDto) ) throw new ArgumentException( "Not PatientUpdateDto" );

            var vm = new PatientUpdateViewModel( patientDto, PatientUpdateDtoValidator, DialogRepository );
            var dialog = DialogRepository.GetView( vm );
            
            if (dialog?.ShowDialog() == true) {
                return ( true, vm.Patient );
            }

            return ( false, vm.Patient );
        }
    }
}
