using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.Domain;

namespace Galchenko.TestTask.ApplicationLayer.Addresses.Dtos
{
    [MapFrom( typeof(Address), ReverseMap = true )]
    public class AddressUpdateDto : AddressNewDto, IEntityIdDto
    {
        public int Id { get; set; }
    }
}
