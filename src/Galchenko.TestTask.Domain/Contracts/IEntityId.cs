
namespace Galchenko.TestTask.Domain.Contracts
{
    public interface IEntityId< TKey > : IEntity
    {
        TKey Id { get; set; }
    }

    public interface IEntityId : IEntityId< int >
    { }
}