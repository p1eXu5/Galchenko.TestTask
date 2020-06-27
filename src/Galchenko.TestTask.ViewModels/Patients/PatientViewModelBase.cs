using System.ComponentModel;
using System.Linq;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using Galchenko.TestTask.Domain.Enums;
using p1eXu5.Wpf.MvvmBaseLibrary;
using DialogViewModel = p1eXu5.Wpf.MvvmLibrary.DialogViewModel;

namespace Galchenko.TestTask.ViewModels.Patients
{
    public abstract class PatientViewModelBase< TDto > : DialogViewModel 
        where TDto : PatientDtoBase
    {
        private readonly IValidator< TDto > _patientValidator;
        private readonly DialogRepository _dialogRepository;

        protected PatientViewModelBase( TDto patient, 
                                        IValidator< TDto > patientValidator, 
                                        DialogRepository dialogRepository )
        {
            Patient = patient;
            _patientValidator = patientValidator;
            _dialogRepository = dialogRepository;

            DateOfBirth = new DateViewModel( patient.DateOfBirth );
            ((INotifyPropertyChanged)DateOfBirth).PropertyChanged += (s, e) => Patient.DateOfBirth = DateOfBirth.Date;
        }


        public virtual TDto Patient { get; }

        public string FirstName
        {
            get => Patient.FirstName;
            set {
                Patient.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => Patient.LastName;
            set {
                Patient.LastName = value;
                OnPropertyChanged();
            }
        }

        public string? MiddleName
        {
            get => Patient.MiddleName;
            set {
                Patient.MiddleName = value;
                OnPropertyChanged();
            }
        }


        public string Phone
        {
            get => Patient.Phone;
            set {
                Patient.Phone = value;
                OnPropertyChanged();
            }
        }


        public Gender Gender
        {
            get => Patient.Gender;
            set {
                Patient.Gender = value;
                OnPropertyChanged();
            }
        }

        public DateViewModel DateOfBirth { get; }


        public override void OnDialogRequestClose( object sender, CloseRequestedEventArgs args )
        {
            if ( args.DialogResult == true ) {
                var result = _patientValidator.Validate( Patient );
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
