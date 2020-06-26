using System;
using System.Linq;
using System.Threading.Tasks;
using Galchenko.TestTask.ApplicationLayer.Common.Models;
using Galchenko.TestTask.Domain.Contracts;

namespace Galchenko.TestTask.ApplicationLayer.Common
{
    public interface IRepository
    {
        Task< TKey > CreateAsync< TEntity, TNewDto, TKey >( TNewDto newDto )
            where TNewDto : notnull
            where TEntity : class, IEntityId< TKey >, new()
            where TKey : notnull;

        Task< TDto[] > GetAllAsync< TEntity, TDto >( Func< IQueryable< TEntity >, IQueryable< TEntity >>? include = null ) 
            where TEntity : class, IEntity 
            where TDto : notnull, IEntityDto;

        Task< (Result, TDto?) > GetByIdAsync< TDto, TEntity, TKey >( TKey id )
            where TDto : class, IEntityDto
            where TEntity : class, IEntityId< TKey >, new()
            where TKey : notnull;

        Task< Result > UpdateAsync< TUpdateDto, TEntity, TKey >( TUpdateDto updateDto )
            where TUpdateDto : class, IEntityIdDto<TKey>
            where TEntity : class, IEntityId< TKey >, new()
            where TKey : notnull;

        Task< Result > DeleteAsync< TEntity, TKey >( TKey id )
            where TEntity : class, IEntityId< TKey >, new()
            where TKey : notnull;
    }
}
