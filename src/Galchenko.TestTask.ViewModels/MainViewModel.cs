using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Accessibility;
using AutoMapper;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Addresses.Dtos;
using Galchenko.TestTask.ApplicationLayer.Appointments;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Patients;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using Galchenko.TestTask.Domain;
using Microsoft.Extensions.Logging;
using p1eXu5.Wpf.MvvmBaseLibrary;
using p1eXu5.Wpf.MvvmLibrary;
using ViewModelBase = p1eXu5.Wpf.MvvmLibrary.ViewModelBase;

namespace Galchenko.TestTask.ViewModels
{
    public class MainViewModel : ViewModelBase, IErrorHandler
    {
        private readonly IRepository _repository;
        private readonly DialogRepository _dialogRepository;
        private readonly ILogger< MainViewModel > _logger;
        private readonly IMapper _mapper;
        private readonly ObservableCollection< PatientRowViewModel > _patientVmCollection;
        private readonly ObservableCollection< AppointmentRowViewModel > _appointmentVmCollection;

        private readonly IValidator< PatientUpdateDto > _patientUpdateDtoValidator;
        private readonly IValidator< PatientNewDto > _patientNewDtoValidator;
        
        private readonly IValidator< AppointmentUpdateDto > _appointmentValidator;

        private PatientRowViewModel? _selectedPatient;

        public MainViewModel( IRepository repository, 
                              DialogRepository dialogRepository, 
                              ILogger< MainViewModel > logger, 
                              IValidator< PatientUpdateDto > patientUpdateDtoValidator,
                              IValidator< PatientNewDto > patientNewDtoValidator,
                              IValidator< AppointmentUpdateDto > appointmentValidator,
                              IMapper mapper )
        {
            _repository = repository;
            _dialogRepository = dialogRepository;
            _appointmentValidator = appointmentValidator;
            _mapper = mapper;
            _logger = logger;

            _patientUpdateDtoValidator = patientUpdateDtoValidator;
            _patientNewDtoValidator = patientNewDtoValidator;

            _patientVmCollection = new ObservableCollection< PatientRowViewModel >();
            PatientVmCollection = new ReadOnlyObservableCollection< PatientRowViewModel >( _patientVmCollection );

            _appointmentVmCollection = new ObservableCollection< AppointmentRowViewModel >();
            AppointmentVmCollection = new ReadOnlyObservableCollection< AppointmentRowViewModel >( _appointmentVmCollection );
        }


        public ReadOnlyObservableCollection< PatientRowViewModel > PatientVmCollection { get; }
        public ReadOnlyObservableCollection< AppointmentRowViewModel > AppointmentVmCollection { get; }

        public PatientRowViewModel? SelectedPatient
        {
            get => _selectedPatient; 
            set {
                _selectedPatient = value;
                OnPropertyChanged();
            }
        }


        #region LoadDataCommand
        public IAsyncCommand LoadDataCommand => new MvvmAsyncCommand( LoadData, errorHandler: this );

        private async Task LoadData( object o )
        {
            await LoadPatients();
            await LoadAppointments();
        }

        private async Task LoadPatients()
        {
            var patients = await _repository.GetAllAsync<Patient, PatientUpdateDto>( Repository.PatientIncludeAddress );

            _patientVmCollection.Clear();
            foreach (var patient in patients) {
                _patientVmCollection.Add( new PatientRowViewModel( patient ) );
            }
        }

        private async Task LoadAppointments()
        {
            var patients = await _repository.GetAllAsync<Appointment, AppointmentViewDto>( Repository.AppointmentIncludePatient );

            _appointmentVmCollection.Clear();
            foreach (var patient in patients) {
                _appointmentVmCollection.Add( new AppointmentRowViewModel( patient ) );
            }
        }

        #endregion


        #region CreatePatientCommand
        public IAsyncCommand CreatePatientCommand => new MvvmAsyncCommand( CreatePatientAsync, errorHandler: this );

        private async Task CreatePatientAsync( object o )
        {
            var newPatient = new PatientNewDto { Address = new AddressNewDto() };

            var vm = new PatientNewViewModel( newPatient, _patientNewDtoValidator, _dialogRepository );
                var dialog = _dialogRepository.GetView( vm );
                
                if (dialog?.ShowDialog() == true) {
                    
                    string? id = await _repository.CreateAsync< Patient, PatientNewDto, string>( vm.Patient );
                    
                    if ( id is {} ) {
                        var (result, patient) = await _repository.GetByIdAsync<PatientUpdateDto, Patient, string>( id );
                        if ( result.Succeeded ) {
                            var patientRow = new PatientRowViewModel( patient! );
                            _patientVmCollection.Add( patientRow );
                            SelectedPatient = patientRow;
                        }
                    }
                    else {
                        ShowError( "An error occurred while creating patient." );
                    }
                }
        }

        #endregion


        #region UpdateCardCommand
        public IAsyncCommand UpdatePatientCommand => new MvvmAsyncCommand( UpdatePatientAsync, errorHandler: this );

        private async Task UpdatePatientAsync( object o )
        {
            var selectedPatient = SelectedPatient;

            if ( selectedPatient is null ) return;

            var (result, patient) = await _repository.GetByIdAsync<PatientUpdateDto, Patient, string>( selectedPatient.Id );

            if (result.Succeeded) 
            {
                var vm = new PatientUpdateViewModel( patient!, _patientUpdateDtoValidator, _dialogRepository );
                var dialog = _dialogRepository.GetView( vm );
                
                if (dialog?.ShowDialog() == true) {
                    
                    result = await _repository.UpdateAsync<PatientUpdateDto, Patient, string>( vm.Patient );
                    
                    if ( result.Succeeded ) {
                        if ( _patientVmCollection.Remove( selectedPatient ) ) {
                            var patientRow = new PatientRowViewModel( vm.Patient );
                            _patientVmCollection.Add( patientRow );
                            SelectedPatient = patientRow;
                        }
                    }
                    else {
                        ShowError( "An error occurred while updating patient data." );
                    }
                }
            }
        }

        #endregion


        #region DeletePatient

        public IAsyncCommand DeletePatientCommand => new MvvmAsyncCommand( DeletePatientAsync, errorHandler: this );

        private async Task DeletePatientAsync( object o )
        {
            var selectedPatient = SelectedPatient;
            if ( selectedPatient is null ) return;

            var result = await _repository.DeleteAsync< Patient, string >( selectedPatient.Id );

            if ( result.Succeeded ) {
                _patientVmCollection.Remove( selectedPatient );
            }
            else {
                ShowError( $"An error occurred while updating patient data. \n { result.Errors }" );
            }
        }

        #endregion


        /// <summary>
        /// Menu command: File -> Exit
        /// </summary>
        public ICommand ExitCommand => new MvvmCommand( o => System.Windows.Application.Current.Shutdown() );





        public void HandleError( Exception ex )
        {
            _logger.Log( LogLevel.Error, ex.Message );
        }


        private void ShowError( string message )
        {
            var vm = new ErrorViewModel( message );
            var dialog = _dialogRepository.GetView( vm );
            dialog?.ShowDialog();
        }
    }
}
