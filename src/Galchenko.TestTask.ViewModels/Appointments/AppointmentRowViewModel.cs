using System;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;
using Galchenko.TestTask.Domain.Enums;
using Galchenko.TestTask.ViewModels.Contracts;
using p1eXu5.Wpf.MvvmLibrary;

namespace Galchenko.TestTask.ViewModels.Appointments
{
    public class AppointmentRowViewModel : ViewModelBase, IIdViewModel< int >
    {
        private readonly AppointmentViewDto _appointment;

        public AppointmentRowViewModel( AppointmentViewDto appointment )
        {
            _appointment = appointment;
        }

        public int Id => _appointment.Id;

        public DateTimeOffset Date => _appointment.Date.LocalDateTime;
        public string Name => $"{_appointment.Patient.LastName} {_appointment.Patient.FirstName} {_appointment.Patient.MiddleName ?? ""}";
        public AppointmentType Type => _appointment.Type;
    }
}
