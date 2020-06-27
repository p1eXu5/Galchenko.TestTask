using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using Galchenko.TestTask.Domain;
using p1eXu5.Wpf.MvvmBaseLibrary;
using p1eXu5.Wpf.MvvmLibrary;
using DialogViewModel = p1eXu5.Wpf.MvvmLibrary.DialogViewModel;

namespace Galchenko.TestTask.ViewModels.Patients
{
    public class PatientListViewModel : DialogViewModel
    {
        private readonly IRepository _repository;
        private readonly DialogRepository _dialogRepository;
        private IEnumerable< PatientRowViewModel > _patients;
        private PatientRowViewModel? _selectedPatient;

        public PatientListViewModel( IRepository repository, DialogRepository dialogRepository )
        {
            _repository = repository;
            _dialogRepository = dialogRepository;
            _patients = new PatientRowViewModel[0];
        }

        public IEnumerable< PatientRowViewModel > Patients
        {
            get => _patients;
            private set {
                _patients = value;
                OnPropertyChanged();
            }
        }

        public PatientRowViewModel? SelectedPatient
        {
            get => _selectedPatient;
            set {
                _selectedPatient = value;
                OnPropertyChanged();
            }
        }

        public IAsyncCommand LoadDataCommand => new MvvmAsyncCommand( LoadDataAsync );

        private async Task LoadDataAsync( object o )
        {
            var patients = await _repository.GetAllAsync< Patient, PatientUpdateDto >( Repository.PatientIncludeAddress );
            SelectedPatient = null;
            Patients = patients.Select( p => new PatientRowViewModel( p ) );
        }


        public override void OnDialogRequestClose( object sender, CloseRequestedEventArgs args )
        {
            if ( args.DialogResult == true ) {
                var sp = SelectedPatient;

                if ( sp is null ) {
                    var evm = new ErrorViewModel( "Select a patient." );
                    var dialog = _dialogRepository.GetView( evm );
                    dialog?.ShowDialog();
                    return;
                }
            }

            base.OnDialogRequestClose( sender, args );
        }
    }
}
