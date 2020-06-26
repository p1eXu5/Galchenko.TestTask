using AutoMapper;

namespace Galchenko.TestTask.ApplicationLayer.Common.Mapper.Converters
{
    public class DtoIdToIdTypeConverter<TDto> : ITypeConverter<TDto, int>
        where TDto : IEntityIdDto
    {
        public int Convert( TDto source, int destination, ResolutionContext context )
        {
            return source.Id;
        }
    }
}
