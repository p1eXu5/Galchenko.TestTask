using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Galchenko.TestTask.ApplicationLayer.Common.Extensions;
using Galchenko.TestTask.ApplicationLayer.Common.Models;
using Galchenko.TestTask.Domain;
using Galchenko.TestTask.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Galchenko.TestTask.ApplicationLayer.Common
{
    public class Repository : IRepository
    {
        private readonly ILogger _logger;

        public Repository( IApplicationDbContext dbContext, IMapper mapper, ILogger< Repository > logger )
        {
            _logger = logger;
            Mapper = mapper;
            DbContext = dbContext;
        }


        public static Func< IQueryable< Patient >, IQueryable< Patient >> PatientIncludeAddress { get; }
            = patients => patients.Include( p => p.Address );

        public static Func< IQueryable< Appointment >, IQueryable< Appointment >> AppointmentIncludePatient { get; }
            = appointment => appointment.Include( p => p.Patient );


        protected IMapper Mapper { get; }
        protected IApplicationDbContext DbContext { get; }


        /// <summary>
        /// Creates entity in db and returns id for created entity.
        /// </summary>
        /// <param name="newDto"></param>
        /// <returns></returns>
        public virtual async Task< TKey > CreateAsync< TEntity, TNewDto, TKey >( TNewDto newDto )
            where TNewDto : notnull
            where TEntity : class, IEntityId< TKey >, new()
            where TKey : notnull
        {
            TEntity entity = Mapper.Map< TEntity >(newDto);
            await DbContext.AddAsync( entity );
            await DbContext.SaveChangesAsync();

            return entity.Id;
        }


        public async Task< TDto[] > GetAllAsync< TEntity, TDto >(
            Func< IQueryable< TEntity >, IQueryable< TEntity >>? include = null
        ) 
            where TEntity : class, IEntity 
            where TDto : notnull, IEntityDto
        {

            IQueryable< TEntity > query = GetQueryable( include );

            TEntity[] entities;

            if ( query.IsImplementAsyncEnumerable() ) {
                entities = await query.ToArrayAsync();
            }
            else {
                entities = query.ToArray();
            }

            var dtos = entities.Select( p => (TDto)Mapper.Map( p, typeof( TEntity ), typeof( TDto ) ) ).ToArray();

            _logger.Log( LogLevel.Information, "=================REPO===================  Return.", typeof(TEntity), typeof( TDto ), Thread.CurrentThread.ManagedThreadId );
            return dtos;
        }


        /// <summary>
        /// Returns entity dto by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task< (Result, TDto?) > GetByIdAsync< TDto, TEntity, TKey >( TKey id )
            where TDto : class, IEntityDto
            where TEntity : class, IEntityId< TKey >, new()
            where TKey : notnull
        {
            var entity = await GetEntityByIdAsync< TEntity, TKey >( id );

            if ( entity != null ) {
                return (Result.Success(), Mapper.Map< TDto >( entity ));
            }
            else {
                return (Result.Failure( new [] { $"Entity with {id.ToString()} does not exist." } ), (TDto?)null);
            }
        }


        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        public virtual async Task< Result > UpdateAsync< TUpdateDto, TEntity, TKey >( TUpdateDto updateDto )
            where TUpdateDto : class, IEntityIdDto<TKey>
            where TEntity : class, IEntityId< TKey >, new()
            where TKey : notnull
        {
            var entity = await GetEntityByIdAsync< TEntity, TKey >( updateDto.Id );

            if ( entity == null ) {
                return Result.Failure( $"Entity with { updateDto.Id.ToString() } does not exist." );
            }

            Mapper.Map( updateDto, entity, typeof( TUpdateDto ), typeof( TEntity ) );
            await DbContext.SaveChangesAsync();
            
            return Result.Success();
        }


        /// <summary>
        /// Returns entity by id or null if there is no entity with current id.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual Task< TEntity > GetEntityByIdAsync< TEntity, TKey >( TKey key )
            where TEntity : class, IEntityId< TKey >, new()
            where TKey : notnull
            => GetQueryable< TEntity >().Where( e => e.Id!.Equals( key ) ).FirstOrDefaultAsync();

        protected internal virtual IQueryable< TEntity > GetQueryable< TEntity >( 
            Func< IQueryable< TEntity >, IQueryable< TEntity >>? include = null
        )
             where TEntity : class, IEntity
        {
            var query = DbContext.Set< TEntity >().AsQueryable();
            if ( include is {} ) {
                query = include( query );
            }

            return query;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task< Result > DeleteAsync< TEntity, TKey >( TKey id )
            where TEntity : class, IEntityId< TKey >, new()
            where TKey : notnull
        {
            var entity = await GetEntityByIdAsync< TEntity, TKey >( id );
            if ( entity == null ) {
                return Result.Failure( $"There is no entity with current id {id}." );
            }
            DbContext.Remove( entity );
            await DbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
