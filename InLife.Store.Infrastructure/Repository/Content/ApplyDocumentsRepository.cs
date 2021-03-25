using System;
using System.Collections.Generic;
using System.Text;
using InLife.Store.Core.Models;
using InLife.Store.Core.Models.ContentEntities;
using InLife.Store.Core.Repository;
using InLife.Store.Core.Repository.Modals;

namespace InLife.Store.Infrastructure.Repository
{
	public class ApplyDocumentsRepository:EntityRepository<ApplyDocuments>, IApplyDocumentsRepository
	{
		public ApplyDocumentsRepository(ApplicationContext context) : base(context)
		{
			InitializeDataSet
			(
				this.dbset
			);
		}
	}
}
