using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;


namespace InLife.Store.Infrastructure.Repository
{
	public class ProductRepository : EntityRepository<Product>, IProductRepository
	{
		public ProductRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.Include(x => x.Details)
					.Include(x => x.CreatedBy)
					.Include(x => x.UpdatedBy)
					.OrderBy(x => x.SortNum)
						.ThenBy(x => x.ProductName)
			);
		}

		// Persistence operation methods only
	}
}
