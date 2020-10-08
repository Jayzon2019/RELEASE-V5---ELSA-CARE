using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class ActivityLogViewModel
	{
		private readonly IActivityLogRepository activityLogRepository;

		public ActivityLogViewModel(IActivityLogRepository activityLogRepository)
		{
			this.activityLogRepository = activityLogRepository;
		}

		public ActivityLogViewModel(ActivityLog model)
		{
			this.Id = model.Id;
			this.ActionPerfomed = model.ActionPerfomed;
			this.ActivityDescription = model.ActivityDescription;
			this.IpAddress = model.IpAddress;
			this.ActivityBy = $"{model.ActivityBy.FirstName} {model.ActivityBy.LastName}".Trim();
			this.ActivityDate = model.ActivityDate?.ToOffset(new TimeSpan(8, 0, 0)).ToString();
			this.ActivityById = model.ActivityById.ToString();
		}

		//public ActivityLog Map()
		//{
		//	var model = new ActivityLog
		//	{
		//		Id = this.Id,
		//		ActionPerfomed = this.ActionPerfomed,
		//		ActivityDescription = this.ActivityDescription,
		//		IpAddress = this.IpAddress,
		//		ActivityDate = this.ActivityDate,
		//		ActivityById = this.ActivityById
		//	};

		//	return model;
		//}

		public int Id { get; set; }

        public string ActivityBy { get; set; }

		public string ActionPerfomed { get; set; }

		public string ActivityDescription { get; set; }

		public string IpAddress { get; set; }

		public string ActivityDate { get; set; }

		public string ActivityById { get; set; }
    }
}
