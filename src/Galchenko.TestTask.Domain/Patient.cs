using System;
using System.Collections.Generic;
using Galchenko.TestTask.Domain.Contracts;
using Galchenko.TestTask.Domain.Enums;

namespace Galchenko.TestTask.Domain
{
    public class Patient : IEntityId< string >
    {
        public Patient()
        {
            Id = Guid.NewGuid().ToString( "D" );
        }

        public string Id { get; set; }

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
