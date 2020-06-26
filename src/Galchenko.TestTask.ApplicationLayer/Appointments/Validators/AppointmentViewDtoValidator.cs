using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using Galchenko.TestTask.ApplicationLayer.Patients.Validators;

namespace Galchenko.TestTask.ApplicationLayer.Appointments.Validators
{
    public class AppointmentViewDtoValidator : AbstractValidator< AppointmentViewDto >
    {
        public AppointmentViewDtoValidator()
        {
            Include( new AppointmentNewDtoValidator() );

            RuleFor( a => a.Patient ).SetValidator( new PatientNewDtoValidator() );
        }
    }
}
