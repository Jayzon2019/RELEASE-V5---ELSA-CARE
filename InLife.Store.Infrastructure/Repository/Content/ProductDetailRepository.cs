using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;


namespace InLife.Store.Infrastructure.Repository
{
	public class ProductDetailRepository : EntityRepository<ProductDetail>, IProductDetailRepository
	{
		public ProductDetailRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.Include(x => x.Product)
					.Include(x => x.CreatedBy)
					.Include(x => x.UpdatedBy)
			);
		}

		// Persistence operation methods only
	}
}
