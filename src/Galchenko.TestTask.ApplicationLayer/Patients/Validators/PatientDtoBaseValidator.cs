using System;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;

namespace Galchenko.TestTask.ApplicationLayer.Patients.Validators
{
    public class PatientDtoBaseValidator : AbstractValidator< PatientDtoBase >
    {
        public PatientDtoBaseValidator()
        {
            RuleFor( p => p.FirstName ).NotEmpty().MaximumLength( 512 );
            RuleFor( p => p.LastName ).NotEmpty().MaximumLength( 512 );
            RuleFor( p => p.MiddleName ).MaximumLength( 512 );
            RuleFor( p => p.Gender ).IsInEnum();
            RuleFor( p => p.DateOfBirth ).InclusiveBetween( new DateTime(1900, 1, 1), DateTime.Now );
            RuleFor( p => p.DateOfBirth ).Configure( r => r.PropertyName = "Date of Birth" );
            RuleFor( p => p.Phone ).NotEmpty().MaximumLength( 32 );
        }
    }
}
