using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;
using Galchenko.TestTask.ApplicationLayer.Appointments.Validators;
using p1eXu5.Wpf.MvvmBaseLibrary;
using ViewModelBase = p1eXu5.Wpf.MvvmLibrary.ViewModelBase;

namespace Galchenko.TestTask.ViewModels.Appointments
{
    public class AppointmentViewModel : DialogViewModel
    {
        private readonly DialogRepository _dialogRepository;
        private readonly IValidator _validator;

        public AppointmentViewModel( AppointmentViewDto appointment, IValidator validator, DialogRepository dialogRepository )
        {
            Appointment = appointment;
            _validator = validator;
            _dialogRepository = dialogRepository;
        }

        public AppointmentViewDto Appointment { get; }


    }
}
