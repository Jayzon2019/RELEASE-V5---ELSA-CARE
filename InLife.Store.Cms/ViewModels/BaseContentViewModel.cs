//TODO: USE AUTOMAPPER

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

			this.CreatedBy = model.CreatedBy?.Id.ToString();
			this.CreatedByName = $"{model.CreatedBy?.FirstName} {model.CreatedBy?.LastName}".Trim();
			this.CreatedDate = model.CreatedDate.ToOffset(new TimeSpan(8, 0, 0)).ToString();

			this.UpdatedBy = model.UpdatedBy?.Id.ToString();
			this.UpdatedByName = $"{model.UpdatedBy?.FirstName} {model.UpdatedBy?.LastName}".Trim();
			this.UpdatedDate = model.UpdatedDate?.ToOffset(new TimeSpan(8, 0, 0)).ToString();
		}

		public int Id { get; set; }

		public string CreatedDate { get; set; }

		public string CreatedBy { get; set; }

		public string CreatedByName { get; set; }

		public string UpdatedDate { get; set; }

		public string UpdatedBy { get; set; }

		public string UpdatedByName { get; set; }
	}
}
