using System.Collections.Generic;
using Galchenko.TestTask.ApplicationLayer.Appointments.Dtos;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.Domain;

namespace Galchenko.TestTask.ApplicationLayer.Patients.Dtos
{
    [MapFrom( typeof( Patient ) )]
    public class PatientViewDto : PatientUpdateDto
    {
        public ICollection< AppointmentUpdateDto >? Appointments { get; set; }
    }
}
