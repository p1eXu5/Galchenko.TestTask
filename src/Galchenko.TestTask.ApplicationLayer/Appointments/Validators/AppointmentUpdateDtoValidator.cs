using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;

namespace Galchenko.TestTask.ApplicationLayer.Appointments.Validators
{
    public class AppointmentUpdateDtoValidator : AbstractValidator< AppointmentUpdateDto >
    {
        public AppointmentUpdateDtoValidator()
        {
            Include( new AppointmentDtoBaseValidator() );

            RuleFor( a => a.Id ).NotEmpty();
        }
    }
}
