using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InLife.Store.Infrastructure.Repository
{
	public interface IContext
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;

		EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

		int SaveChanges();
	}
}
