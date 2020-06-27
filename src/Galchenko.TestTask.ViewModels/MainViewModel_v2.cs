using System.Threading.Tasks;
using System.Windows.Input;
using Galchenko.TestTask.ViewModels.Appointments;
using Galchenko.TestTask.ViewModels.Patients;
using p1eXu5.Wpf.MvvmBaseLibrary;
using p1eXu5.Wpf.MvvmLibrary;
using ViewModelBase = p1eXu5.Wpf.MvvmLibrary.ViewModelBase;

namespace Galchenko.TestTask.ViewModels
{
    public class MainViewModel_v2 : ViewModelBase
    {
        public MainViewModel_v2( PatientCrudViewModel patientCrudVm, AppointmentCrudViewModel appointmentCrudVm )
        {
            PatientCrudVm = patientCrudVm;
            AppointmentCrudVm = appointmentCrudVm;
        }

        public PatientCrudViewModel PatientCrudVm { get; }
        public AppointmentCrudViewModel AppointmentCrudVm { get; }


        public IAsyncCommand LoadDataCommand => new MvvmAsyncCommand( LoadDataAsync );

        private async Task LoadDataAsync( object o )
        {
            await PatientCrudVm.LoadDataCommand.ExecuteAsync( o );
            await AppointmentCrudVm.LoadDataCommand.ExecuteAsync( o );
        }

        /// <summary>
        /// Menu command: File -> Exit
        /// </summary>
        public ICommand ExitCommand => new MvvmCommand( o => System.Windows.Application.Current.Shutdown() );
    }
}
