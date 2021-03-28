using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using InLife.Store.Core.Models;

namespace InLife.Store.Core.Repository
{
	public interface IPrimeSecureApplicationRepository : IEntityRepository<PrimeSecureApplication>
	{
		PrimeSecureApplication GetByReferenceCode(string refcode);
	}
}
