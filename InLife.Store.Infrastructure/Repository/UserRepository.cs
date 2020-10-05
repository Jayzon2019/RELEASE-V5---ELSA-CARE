using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Infrastructure.Repository
{
	public class UserRepository : EntityRepository<User>, IUserRepository
	{
		public UserRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.OrderBy(x => x.LastName)
					.OrderBy(x => x.FirstName)
			);
		}
	}
}
