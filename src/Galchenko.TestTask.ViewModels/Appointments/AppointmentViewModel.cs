using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using Galchenko.TestTask.Domain;
using Galchenko.TestTask.Domain.Enums;
using Galchenko.TestTask.ViewModels.Patients;
using p1eXu5.Wpf.MvvmBaseLibrary;
using p1eXu5.Wpf.MvvmLibrary;
using DialogViewModel = p1eXu5.Wpf.MvvmLibrary.DialogViewModel;

namespace Galchenko.TestTask.ViewModels.Appointments
{
    public class AppointmentViewModel : DialogViewModel
    {
        private readonly DialogRepository _dialogRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public AppointmentViewModel( AppointmentViewDto appointment, IValidator validator, DialogRepository dialogRepository, IRepository repository, IMapper mapper )
        {
            Appointment = appointment;
            Repository = repository;
            _validator = validator;
            _dialogRepository = dialogRepository;
            _mapper = mapper;
            var year = DateTime.Now.Year;
            Date = new DateViewModel( Appointment.Date.LocalDateTime ) { MinYear = year - 5, MaxYear = year + 1 };
            ((INotifyPropertyChanged)Date).PropertyChanged += (s, e) => Appointment.Date = new DateTimeOffset( Date.Date );
        }

        public AppointmentViewDto Appointment { get; }
        public IRepository Repository { get; }


        public ICommand SelectPatientCommand => new MvvmCommand( SelectPatient, CanSelectPatient );


        private void SelectPatient( object o )
        {
            var vm = new PatientListViewModel( Repository, _dialogRepository );
            var dialog = _dialogRepository.GetView( vm );

            if ( dialog?.ShowDialog() == true ) 
            {
                Appointment.Patient = _mapper.Map< PatientNewDto >( vm.SelectedPatient!.Patient );
                Appointment.PatientId = vm.SelectedPatient!.Id;
                OnPropertyChanged( nameof(Name) );
            }
        }

        private bool CanSelectPatient( object o ) => String.IsNullOrWhiteSpace( Appointment.PatientId );


        public string Name => 
            Appointment.Patient is null
                ? "<select from list>"
                : $"{Appointment.Patient.LastName} {Appointment.Patient.FirstName} {Appointment.Patient.MiddleName ?? ""}";

        public DateViewModel Date { get; }

        public AppointmentType Type
        {
            get => Appointment.Type;
            set {
                Appointment.Type = value;
                OnPropertyChanged();
            }
        }

        public string Diagnosis
        {
            get => Appointment.Diagnosis;
            set {
                Appointment.Diagnosis = value;
                OnPropertyChanged();
            }
        }

        public override void OnDialogRequestClose( object sender, CloseRequestedEventArgs args )
        {
            if ( args.DialogResult == true ) {
                var result = _validator.Validate( Appointment );
                if ( !result.IsValid ) {
                    var evm = new ErrorViewModel( result.Errors.Select( e => $"{e.PropertyName} - {e.ErrorMessage} \n" ).Aggregate( "", (acc, val) => acc + val ) );
                    var dialog = _dialogRepository.GetView( evm );
                    dialog?.ShowDialog();
                    return;
                }
            }

            base.OnDialogRequestClose( sender, args );
        }
    }
}
