using System;
using InLife.Store.Core.Models;
using InLife.Store.Core.Models.ContentEntities;

namespace InLife.Store.Core.Business
{
	public interface IApplyDocumentService
	{
		ApplyDocuments SaveApplyDocuments(ApplyDocuments applyDocuments);
	}
}
