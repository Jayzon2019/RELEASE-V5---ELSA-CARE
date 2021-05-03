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
		protected IContext context;
		protected DbSet<T> dbset;

		protected IQueryable<T> dataset;

		public EntityRepository(IContext context)
		{
			this.context = context
				?? throw new ArgumentNullException("Context cannot be null");

			this.dbset = this.context.Set<T>();
		}

		#region Data Access

		public virtual T Get(object id)
		{
			//This is more efficient
			//return this.dbset.Find(id);

			//But I can use include here XD
			//return this.dbset.FirstOrDefault(x => x.Id == id);
			return this.dataset.FirstOrDefault(x => x.Id == id);
		}

		public virtual T Find(object id)
		{
			return this.dbset.Find(id);
		}

		public virtual IQueryable<T> GetAll()
		{
			return this.dataset;
		}

		#endregion Data Access

		#region Data Manipulation

		public virtual void Create(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("Cannot create null entity");

			this.dbset.Add(entity);
			this.context.SaveChanges();
		}

		public virtual void Create(ICollection<T> entities)
		{
			if (entities == null)
				throw new ArgumentNullException("Cannot create entities from a null collection");

			this.dbset.AddRange(entities);
			this.context.SaveChanges();
		}

		public virtual void Update(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("Cannot update null entity");

			this.dbset.Update(entity);
			this.context.SaveChanges();
		}

		public virtual void Update(ICollection<T> entities)
		{
			if (entities == null)
				throw new ArgumentNullException("Cannot update entities from a null collection");

			this.dbset.UpdateRange(entities);
			this.context.SaveChanges();
		}

		public virtual void Delete(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("Cannot delete null entity");

			this.dbset.Remove(entity);
			this.context.SaveChanges();
		}

		public virtual void Delete(ICollection<T> entities)
		{
			if (entities == null)
				throw new ArgumentNullException("Cannot delete entities from a null collection");

			this.dbset.RemoveRange(entities);
			this.context.SaveChanges();
		}

		public virtual int SaveChanges()
		{
			return this.context.SaveChanges();
		}

		#endregion Data Manipulation

		protected virtual void InitializeDataSet(IQueryable<T> dataset)
		{
			this.dataset = dataset;
		}
	}
}
