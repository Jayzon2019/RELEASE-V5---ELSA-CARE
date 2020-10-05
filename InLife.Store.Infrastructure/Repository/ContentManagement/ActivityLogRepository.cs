using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;


namespace InLife.Store.Infrastructure.Repository
{
	public class ActivityLogRepository : EntityRepository<ActivityLog>, IActivityLogRepository
	{
		public ActivityLogRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
					.Include(x => x.ActivityBy)
					.OrderByDescending(x => x.ActivityDate)
			);
		}

		// Persistence operation methods only
	}
}
