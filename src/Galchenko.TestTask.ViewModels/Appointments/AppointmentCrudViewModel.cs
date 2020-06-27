using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.Domain;
using Galchenko.TestTask.ViewModels.Contracts;
using Microsoft.Extensions.Logging;
using p1eXu5.Wpf.MvvmBaseLibrary;
using p1eXu5.Wpf.MvvmLibrary;

namespace Galchenko.TestTask.ViewModels.Appointments
{
    public class AppointmentCrudViewModel : CrudViewModel< AppointmentRowViewModel, AppointmentNewDto, AppointmentUpdateDto, Appointment, int >
    {
        public AppointmentCrudViewModel( IRepository repository, 
                                         DialogRepository dialogRepository, 
                                         ILogger< AppointmentCrudViewModel > logger, 
                                         IMapper mapper,
                                         IValidator< AppointmentUpdateDto > appointmentUpdateDtoValidator,
                                         IValidator< AppointmentNewDto > appointmentNewDtoValidator
                                         ) 
            : base( repository, dialogRepository, logger, mapper )
        {
            AppointmentUpdateDtoValidator = appointmentUpdateDtoValidator;
            AppointmentNewDtoValidator = appointmentNewDtoValidator;
        }


        protected override Func< IQueryable< Appointment >, IQueryable< Appointment > >? LoadInclude { get; } 
            = ApplicationLayer.Common.Repository.AppointmentIncludePatient;

        public IValidator< AppointmentUpdateDto > AppointmentUpdateDtoValidator { get; }
        public IValidator< AppointmentNewDto > AppointmentNewDtoValidator { get; }


        protected override Task LoadDataAsync( object o )
        {
            return FillVmCollectionAsync< AppointmentViewDto, AppointmentRowViewModel >();
        }


        protected override (bool, AppointmentNewDto) CreateNewDto()
        {
            var appointmentDto = new AppointmentViewDto();

            var vm = new AppointmentViewModel( appointmentDto, AppointmentNewDtoValidator, DialogRepository );
            var dialog = DialogRepository.GetView( vm );
            
            if (dialog?.ShowDialog() == true) {
                appointmentDto = vm.Appointment;
                var newDto = Mapper.Map< AppointmentNewDto >( appointmentDto );
                return (true, newDto);
            }

            return (false, appointmentDto);
        }

        public override IAsyncCommand UpdateCommand  => new MvvmAsyncCommand( UpdateAsync< AppointmentViewDto >, errorHandler: this );

        protected override (bool, AppointmentUpdateDto) UpdateDto< TDto >( TDto dto )
        {
            if ( !(dto is AppointmentViewDto appointmentDto) ) throw new ArgumentException( "Not AppointmentViewDto" );

            var vm = new AppointmentViewModel( appointmentDto, AppointmentUpdateDtoValidator, DialogRepository );
            var dialog = DialogRepository.GetView( vm );
            var res = false;

            if (dialog?.ShowDialog() == true) {
                appointmentDto = vm.Appointment;
                res = true;
            }

            var newDto = Mapper.Map< AppointmentUpdateDto >( appointmentDto );
            return ( res, newDto );
        }
    }
}
