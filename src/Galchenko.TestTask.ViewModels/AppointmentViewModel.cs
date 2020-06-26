using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;
using p1eXu5.Wpf.MvvmBaseLibrary;
using ViewModelBase = p1eXu5.Wpf.MvvmLibrary.ViewModelBase;

namespace Galchenko.TestTask.ViewModels
{
    public class AppointmentViewModel : DialogViewModel
    {
        private readonly DialogRepository _dialogRepository;

        public AppointmentViewModel( AppointmentUpdateDto appointment, DialogRepository dialogRepository )
        {
            Appointment = appointment;
            _dialogRepository = dialogRepository;
        }

        public AppointmentUpdateDto Appointment { get; }


    }
}
