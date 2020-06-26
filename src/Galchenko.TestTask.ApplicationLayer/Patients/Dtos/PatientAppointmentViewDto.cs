using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.Domain;

namespace Galchenko.TestTask.ApplicationLayer.Patients.Dtos
{
    [MapFrom( typeof(Patient) )]
    public class PatientAppointmentViewDto : IEntityDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? MiddleName { get; set; }
    }
}
