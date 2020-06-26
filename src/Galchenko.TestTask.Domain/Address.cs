
using Galchenko.TestTask.Domain.Contracts;

namespace Galchenko.TestTask.Domain
{
    public class Address : IEntityId
    {
        public int Id { get; set; }

        public string Line1 { get; set; } = default!;
        public string? Line2 { get; set; }
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
    }
}
