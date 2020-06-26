using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Addresses.Dtos;

namespace Galchenko.TestTask.ApplicationLayer.Addresses.Validators
{
    public class AddressNewDtoValidator : AbstractValidator< AddressNewDto >
    {
        public AddressNewDtoValidator()
        {
            RuleFor( a => a.City ).NotEmpty().MaximumLength( 256 );
            RuleFor( a => a.Line1 ).NotEmpty().MaximumLength( 512 );
            RuleFor( a => a.PostalCode ).NotEmpty().MaximumLength( 16 );
            RuleFor( a => a.Line2 ).MaximumLength( 512 );
        }
    }
}
