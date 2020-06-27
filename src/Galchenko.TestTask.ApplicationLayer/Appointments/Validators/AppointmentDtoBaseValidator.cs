using System;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;

namespace Galchenko.TestTask.ApplicationLayer.Appointments.Validators
{
    public class AppointmentDtoBaseValidator : AbstractValidator< AppointmentDtoBase >
    {
        public AppointmentDtoBaseValidator()
        {
            RuleFor( a => a.Date ).InclusiveBetween( new DateTimeOffset( 1971, 1, 1, 8, 0, 0, TimeSpan.Zero), DateTimeOffset.UtcNow + TimeSpan.FromDays( 830 ) );
            RuleFor( a => a.Diagnosis ).NotEmpty();
            RuleFor( a => a.Type ).IsInEnum();
        }
    }
}
