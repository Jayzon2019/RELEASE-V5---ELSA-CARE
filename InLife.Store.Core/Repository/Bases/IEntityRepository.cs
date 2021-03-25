using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using InLife.Store.Core.Models;

namespace InLife.Store.Core.Repository
{
	public interface IEntityRepository<T> where T : BaseEntity
	{
		void Create(T entity);

		void Create(ICollection<T> entities);

		void Update(T entity);

		void Update(ICollection<T> entities);

		void Delete(T entity);

		void Delete(ICollection<T> entities);

		int SaveChanges();

		/// <summary>
		///		Get the entity that matches the provided Id
		/// </summary>
		/// <returns>Single entity</returns>
		T Get(object id);

		T Find(object id);


		/// <summary>
		///		Get all entities
		/// </summary>
		IQueryable<T> GetAll();
	}
}
