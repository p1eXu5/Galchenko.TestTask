using Galchenko.TestTask.ApplicationLayer.Addresses.Dtos;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.Domain;

namespace Galchenko.TestTask.ApplicationLayer.Patients.Dtos
{
    [MapFrom( typeof(Patient), ReverseMap = true )]
    public class PatientUpdateDto : PatientDtoBase, IEntityIdDto< string >
    {
        public string Id { get; set; } = default!;

        public AddressUpdateDto Address { get; set; } = default!;
    }
}
