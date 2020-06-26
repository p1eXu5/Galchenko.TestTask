using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.Domain;

namespace Galchenko.TestTask.ApplicationLayer.Appointments.Dtos
{
    [MapFrom( typeof(Appointment), ReverseMap = true )]
    [MapFrom( typeof( AppointmentViewDto ) )]
    public class AppointmentUpdateDto : AppointmentDtoBase, IEntityIdDto
    {
        public int Id { get; set; }
    }
}
