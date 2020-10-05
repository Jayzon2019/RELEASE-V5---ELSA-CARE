using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class BaseContentViewModel
	{
		public BaseContentViewModel()
		{
		}

		public BaseContentViewModel(BaseContentEntity model)
		{
			this.Id = model.Id;

			this.CreatedBy = (model.CreatedBy == null)
				? (Guid?)null
				: model.CreatedBy.Id;
			this.CreatedByName = (model.CreatedBy == null)
				? null
				: $"{model.CreatedBy.FirstName} {model.CreatedBy.LastName}".Trim();
			this.CreatedDate = model.CreatedDate;

			this.UpdatedBy = (model.UpdatedBy == null)
				? (Guid?)null
				: model.UpdatedBy.Id;
			this.UpdatedByName = (model.UpdatedBy == null)
				? null
				: $"{model.UpdatedBy.FirstName} {model.UpdatedBy.LastName}".Trim();
			this.UpdatedDate = model.UpdatedDate;
		}

		public int Id { get; set; }

		public DateTimeOffset CreatedDate { get; set; }

		public Guid? CreatedBy { get; set; }

		public string CreatedByName { get; set; }

		public DateTimeOffset? UpdatedDate { get; set; }

		public Guid? UpdatedBy { get; set; }

		public string UpdatedByName { get; set; }
	}
}
