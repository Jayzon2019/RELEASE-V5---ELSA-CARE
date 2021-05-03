using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;


namespace InLife.Store.Infrastructure.Repository
{
	public class PrimeSecureApplicationRepository : EntityRepository<PrimeSecureApplication>, IPrimeSecureApplicationRepository
	{
		public PrimeSecureApplicationRepository(PrimeSecureContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
			);
		}

		public PrimeSecureApplication GetByReferenceCode(string refcode)
		{
			//return this.dataset.FirstOrDefault(x => String.Equals(x.ReferenceCode, refcode, StringComparison.OrdinalIgnoreCase));
			return this.dataset.FirstOrDefault(x => x.ReferenceCode.ToLower() == refcode.ToLower());
		}
	}
}
