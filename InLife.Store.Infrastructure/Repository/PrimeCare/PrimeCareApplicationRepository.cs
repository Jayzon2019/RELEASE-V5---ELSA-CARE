using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;


namespace InLife.Store.Infrastructure.Repository
{
	public class PrimeCareApplicationRepository : EntityRepository<PrimeCareApplication>, IPrimeCareApplicationRepository
	{
		public PrimeCareApplicationRepository(PrimeCareContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.Include(x => x.Customer)
						.ThenInclude(x => x.HomeAddress)
			);
		}

		// Persistence operation methods only

		public PrimeCareApplication GetByReferenceCode(string refcode)
		{
			return this.dataset.FirstOrDefault(x => String.Compare(x.ReferenceCode, refcode, true) == 0);
		}
	}
}
