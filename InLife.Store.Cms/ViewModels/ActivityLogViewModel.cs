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
			this.Action = model.Action;
			this.Description = model.Description;
			this.TransactionById = model.TransactionBy?.Id;
			this.TransactionByName = model.TransactionByName;
			this.TransactionDate = model.TransactionDate?.ToOffset(new TimeSpan(8, 0, 0)).ToString("yyyy-MM-dd hh:mm tt");
			this.TransactionRemoteAddress = model.TransactionRemoteAddress;
		}

		public int Id { get; set; }

		public string Action { get; set; }

		public string Description { get; set; }

		public string TransactionById { get; set; }

		public string TransactionByName { get; set; }

		public string TransactionDate { get; set; }

		public string TransactionRemoteAddress { get; set; }
	}
}
