using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Galchenko.TestTask.ApplicationLayer.Common.Validators
{
    public class PatientIdValidator : AbstractValidator< string >
    {
        public PatientIdValidator()
        {
            RuleFor( s => s ).NotEmpty().MinimumLength( 36 ).MaximumLength( 36 );
        }
    }
}
