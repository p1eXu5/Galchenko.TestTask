using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Addresses.Dtos;

namespace Galchenko.TestTask.ApplicationLayer.Addresses.Validators
{
    public class AddressUpdateDtoValidator : AbstractValidator< AddressUpdateDto >
    {
        public AddressUpdateDtoValidator()
        {
            Include( new AddressNewDtoValidator() );

            RuleFor( a => a.Id ).NotEmpty();
        }
    }
}
