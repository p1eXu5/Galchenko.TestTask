using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Addresses.Validators;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;

namespace Galchenko.TestTask.ApplicationLayer.Patients.Validators
{
    public class PatientNewDtoValidator : AbstractValidator< PatientNewDto >
    {
        public PatientNewDtoValidator()
        {
            Include( new PatientDtoBaseValidator() );
            RuleFor( p => p.Address ).SetValidator( new AddressNewDtoValidator() );
        }
    }
}
