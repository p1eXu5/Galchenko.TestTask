using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.Domain;

namespace Galchenko.TestTask.ApplicationLayer.Addresses.Dtos
{
    [MapTo( typeof(Address), ReverseMap = true )]
    public class AddressNewDto : IEntityDto
    {
        public string Line1 { get; set; } = default!;
        public string? Line2 { get; set; }
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
    }
}
