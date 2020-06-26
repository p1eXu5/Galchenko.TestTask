
namespace Galchenko.TestTask.ApplicationLayer.Common.Models
{
    public class EntityIdDto : IEntityIdDto
    {
        public int Id { get; set; }

        public static implicit operator int( EntityIdDto dto ) => dto.Id;
        public static implicit operator EntityIdDto( int id ) => new EntityIdDto() { Id = id };

        public static implicit operator int?( EntityIdDto? dto ) => dto?.Id;
        public static implicit operator EntityIdDto?( int? id ) => id != null ? new EntityIdDto(){ Id = id.Value } : null;
    }
}