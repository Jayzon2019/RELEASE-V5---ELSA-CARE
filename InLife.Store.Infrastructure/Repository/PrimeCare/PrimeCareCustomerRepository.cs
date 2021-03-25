using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;


namespace InLife.Store.Infrastructure.Repository
{
	public class PrimeCarePersonRepository : EntityRepository<PrimeCarePerson>, IPrimeCarePersonRepository
	{
		public PrimeCarePersonRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.Include(x => x.BirthAddress)
					.Include(x => x.HomeAddress)
					.Include(x => x.WorkAddress)
			//.Include(x => x.IdentityDocument)
			//.Include(x => x.SecoondaryIdentityDocument)
			);
		}

	}
}
