using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Addresses.Validators;
using Galchenko.TestTask.ApplicationLayer.Common.Validators;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;

namespace Galchenko.TestTask.ApplicationLayer.Patients.Validators
{
    public class PatientUpdateDtoValidator : AbstractValidator< PatientUpdateDto >
    {
        public PatientUpdateDtoValidator()
        {
            Include( new PatientDtoBaseValidator() );

            RuleFor( p => p.Id ).SetValidator( new PatientIdValidator() );
            RuleFor( p => p.Address ).SetValidator( new AddressUpdateDtoValidator() );
        }
    }
}
