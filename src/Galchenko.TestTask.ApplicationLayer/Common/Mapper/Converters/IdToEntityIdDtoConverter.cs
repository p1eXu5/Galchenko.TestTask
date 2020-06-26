using AutoMapper;
using Galchenko.TestTask.ApplicationLayer.Common.Models;

namespace Galchenko.TestTask.ApplicationLayer.Common.Mapper.Converters
{
    public class IdToEntityIdDtoConverter : ITypeConverter< int?, EntityIdDto? >
    {
        public EntityIdDto? Convert( int? source, EntityIdDto? destination, ResolutionContext context )
        {
            return source;
        }
    }
}