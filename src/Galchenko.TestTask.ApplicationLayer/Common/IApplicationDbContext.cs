using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Galchenko.TestTask.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Galchenko.TestTask.ApplicationLayer.Common
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet< Appointment > Appointments { get; set; }
        DbSet< Patient > Patients { get; set; }

#nullable disable
        Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database { get; }

        DbSet< TEntity > Set< TEntity >() where TEntity : class;
        EntityEntry Entry( object entity );
        EntityEntry< TEntity > Entry< TEntity >( TEntity entity ) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync( CancellationToken cancellationToken = new CancellationToken() );
        void AddRange( IEnumerable< Object > entities );
        EntityEntry< TEntity > Add< TEntity >( TEntity entity )
            where TEntity : class;
        Task AddRangeAsync( IEnumerable< object > entities, CancellationToken cancellationToken = new CancellationToken() );
        ValueTask< EntityEntry<TEntity> > AddAsync<TEntity>( TEntity entity, CancellationToken cancellationToken = new CancellationToken() )
            where TEntity : class;
        EntityEntry< TEntity > Remove< TEntity >( TEntity entity ) where TEntity : class;
        void RemoveRange( params object[] entities );
#nullable restore
    }
}
