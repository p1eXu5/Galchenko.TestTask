using System;
using System.Collections.Generic;
using System.Text;

namespace Galchenko.TestTask.Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public AppointmentType Type { get; set; }
        public string Diagnosis { get; set; } = default!;

        public string PatientId { get; set; } = default!;
        public Patient Patient { get; set; } = default!;
    }
}
