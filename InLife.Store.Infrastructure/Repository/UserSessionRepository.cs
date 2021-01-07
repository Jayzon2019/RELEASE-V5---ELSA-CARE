using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Infrastructure.Repository
{
	public class UserSessionRepository : EntityRepository<UserSession>, IUserSessionRepository
	{
		public UserSessionRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.Include(x => x.User)
			);
		}
	}
}
