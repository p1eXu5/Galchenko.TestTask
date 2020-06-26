using System;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.Domain.Enums;

namespace Galchenko.TestTask.ApplicationLayer.Appointments.Dtos
{
    public class AppointmentDtoBase : IEntityDto
    {
         public DateTimeOffset Date { get; set; }
         public AppointmentType Type { get; set; }
         public string Diagnosis { get; set; } = default!;
    }
}
