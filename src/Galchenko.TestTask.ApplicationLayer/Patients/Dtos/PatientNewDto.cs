using Galchenko.TestTask.ApplicationLayer.Addresses.Dtos;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.Domain;

namespace Galchenko.TestTask.ApplicationLayer.Patients.Dtos
{
    [MapTo( typeof(Patient), ReverseMap = true )]
    public class PatientNewDto : PatientDtoBase
    {
        public AddressNewDto Address { get; set; } = default!;
    }
}
