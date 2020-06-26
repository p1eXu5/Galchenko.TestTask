using Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes;
using Galchenko.TestTask.Domain.Contracts;

namespace Galchenko.TestTask.ApplicationLayer.Common
{
    public interface IEntityDto
    {
    }

    [MapTo(typeof(IEntityId<>), IncludeAllDerived = true, ReverseMap = true, IncludeAllDerivedForReverse = true )]
    public interface IEntityIdDto< TKey > : IEntityDto
    {
        public TKey Id { get; set; }
    }

    [MapTo(typeof(IEntityId), IncludeAllDerived = true, ReverseMap = true, IncludeAllDerivedForReverse = true )]
    public interface IEntityIdDto : IEntityIdDto< int >
    { }

    public interface IEntityNameDto
    {
        string Name { get; }
    }
}
