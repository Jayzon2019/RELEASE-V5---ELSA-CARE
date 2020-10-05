using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Infrastructure.Repository
{
	public abstract class EntityRepository<T> : IEntityRepository<T>
		where T : BaseEntity
	{
		protected IApplicationContext context;
		protected DbSet<T> dbset;

		protected IQueryable<T> dataset;

		public EntityRepository(IApplicationContext context)
		{
			this.context = context
				?? throw new ArgumentNullException("Context cannot be null");

			this.dbset = this.context.Set<T>();
		}

		public virtual T Get(object id)
		{
			return this.dbset.Find(id);
		}

		public virtual void Create(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("Cannot create null entity");

			this.dbset.Add(entity);
			this.context.SaveChanges();
		}


		public virtual void Update(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("Cannot update null entity");

			this.dbset.Update(entity);
			this.context.SaveChanges();
		}

		public virtual void Delete(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("Cannot delete null entity");

			this.dbset.Remove(entity);
			this.context.SaveChanges();
		}

		public virtual IQueryable<T> GetAll()
		{
			return this.dataset;
		}

		protected virtual void InitializeDataSet(IQueryable<T> dataset)
		{
			this.dataset = dataset;
		}
	}
}
