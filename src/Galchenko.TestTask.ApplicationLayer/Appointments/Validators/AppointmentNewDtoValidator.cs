using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;
using Galchenko.TestTask.ApplicationLayer.Common.Validators;

namespace Galchenko.TestTask.ApplicationLayer.Appointments.Validators
{
    public class AppointmentNewDtoValidator : AbstractValidator< AppointmentNewDto >
    {
        public AppointmentNewDtoValidator()
        {
            Include( new AppointmentDtoBaseValidator() );

            RuleFor( a => a.PatientId ).SetValidator( new PatientIdValidator() );
        }   
    }
}
