using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.Domain;

namespace Galchenko.TestTask.ApplicationLayer.Appointments.Dtos
{
    [MapTo( typeof( Appointment ) )]
    public class AppointmentNewDto : AppointmentDtoBase
    {
        public string PatientId { get; set; } = default!;
    }
}
