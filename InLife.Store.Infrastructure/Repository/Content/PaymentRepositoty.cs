

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;
using InLife.Store.Core.Models.ContentEntities;

namespace InLife.Store.Infrastructure.Repository
{
	public class PaymentRepositoty : EntityRepository<PaymentStatus>, IPayMentRepository
	{
		public PaymentRepositoty(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
			);
		}

		// Persistence operation methods only
	}
}

