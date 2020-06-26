using System.Collections.ObjectModel;
using System.Linq;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using p1eXu5.Wpf.MvvmBaseLibrary;

namespace Galchenko.TestTask.ViewModels
{
    public class PatientAppointmentsViewModel
    {
        private readonly DialogRepository _dialogRepository;
        private readonly ObservableCollection< AppointmentViewModel > _appointments;

        public PatientAppointmentsViewModel( PatientViewDto patient, AbstractValidator< PatientAppointmentViewDto > patientValidator, DialogRepository dialogRepository )
        {
            _dialogRepository = dialogRepository;
            _appointments = new ObservableCollection< AppointmentViewModel >(
                patient.Appointments.Select( appointment => new AppointmentViewModel( appointment, _dialogRepository ) )
            );
        }
    }
}
