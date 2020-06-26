using System;
using Galchenko.TestTask.Domain.Contracts;
using Galchenko.TestTask.Domain.Enums;

namespace Galchenko.TestTask.Domain
{
    public class Appointment : IEntityId
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public AppointmentType Type { get; set; }
        public string Diagnosis { get; set; } = default!;

        public string PatientId { get; set; } = default!;
        public Patient Patient { get; set; } = default!;
    }
}
