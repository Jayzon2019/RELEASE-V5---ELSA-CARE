using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;

namespace InLife.Store.Cms.ViewModels
{
	public class ActivityLogViewModel
	{
		public ActivityLogViewModel()
		{
		}

		public ActivityLogViewModel(ActivityLog model)
		{
			this.Id = model.Id;
			this.ActivityBy = $"{model.ActivityBy.FirstName} {model.ActivityBy.LastName}".Trim();
			this.ActionPerfomed = model.ActionPerfomed;
			this.ActivityDescription = model.ActivityDescription;
			this.IpAddress = model.IpAddress;
			this.ActivityDate = model.ActivityDate;
			this.ActivityById = model.ActivityById;
		}

		public ActivityLog Map()
		{
			var model = new ActivityLog
			{
				Id = this.Id,
				ActionPerfomed = this.ActionPerfomed,
				ActivityDescription = this.ActivityDescription,
				IpAddress = this.IpAddress,
				ActivityDate = this.ActivityDate,
				ActivityById = this.ActivityById
		};

			return model;
		}

		public int Id { get; set; }

        public string ActivityBy { get; set; }

		public string ActionPerfomed { get; set; }

		public string ActivityDescription { get; set; }

		public string IpAddress { get; set; }

		public DateTimeOffset? ActivityDate { get; set; }

		public Guid? ActivityById { get; set; }
    }
}
