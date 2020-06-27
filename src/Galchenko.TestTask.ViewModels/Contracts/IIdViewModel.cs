
namespace Galchenko.TestTask.ViewModels.Contracts
{
    public interface IIdViewModel< out TKey >
        where TKey : notnull
    {
        TKey Id { get; }
    }
}
