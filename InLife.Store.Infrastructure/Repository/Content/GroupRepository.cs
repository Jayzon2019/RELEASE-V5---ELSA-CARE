using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;


namespace InLife.Store.Infrastructure.Repository
{
	public class GroupRepository : EntityRepository<GroupsT>, IGroupRepository
	{
		public GroupRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
			);
		}

		// Persistence operation methods only
	}
}
