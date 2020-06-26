using System;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.Domain.Enums;

namespace Galchenko.TestTask.ApplicationLayer.Patients.Dtos
{
    public class PatientDtoBase : IEntityDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? MiddleName { get; set; }
        public string Phone { get; set; } = default!;
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
