using Microsoft.EntityFrameworkCore;

namespace Application.Common.DataContext
{
    public interface IDataContext 
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        IQueryable<TEnt> ReadSet<TEnt>() where TEnt : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
