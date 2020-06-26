using System;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;
using Galchenko.TestTask.Domain.Enums;
using p1eXu5.Wpf.MvvmLibrary;

namespace Galchenko.TestTask.ViewModels
{
    public class AppointmentRowViewModel : ViewModelBase
    {
        private readonly AppointmentViewDto _appointment;

        public AppointmentRowViewModel( AppointmentViewDto appointment )
        {
            _appointment = appointment;
        }

        public DateTimeOffset Date => _appointment.Date;
        public string Name => $"{_appointment.Patient.LastName} {_appointment.Patient.FirstName} {_appointment.Patient.MiddleName ?? ""}";
        public AppointmentType Type => _appointment.Type;
    }
}
