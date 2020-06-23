using System;
using System.Collections.Generic;
using System.Text;

namespace Galchenko.TestTask.Domain
{
    public class Patient
    {
        public string Id { get; } = Guid.NewGuid().ToString( "D" );

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? MiddleName { get; set; }
        public string Phone { get; set; } = default!;
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; } = default!;
        public ICollection< Appointment >? Appointments { get; set; }
    }
}
