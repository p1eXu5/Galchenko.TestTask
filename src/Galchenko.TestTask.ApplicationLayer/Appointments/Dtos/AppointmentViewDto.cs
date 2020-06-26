using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using Galchenko.TestTask.Domain;

namespace Galchenko.TestTask.ApplicationLayer.Appointments.Dtos
{
    [MapFrom( typeof(Appointment) )]
    public class AppointmentViewDto : AppointmentNewDto, IEntityIdDto
    {
        public int Id { get; set; }
        public PatientNewDto Patient { get; set; } = default!;
    }
}
