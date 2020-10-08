using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class KeyMetricViewModel
	{
		private readonly IKeyMetricRepository keyMetricRepository;

		public KeyMetricViewModel(IKeyMetricRepository keyMetricRepository)
		{
			this.keyMetricRepository = keyMetricRepository;
		}

		public KeyMetricViewModel()
		{
		}

		public KeyMetricViewModel(KeyMetric model)
		{
			this.Id = model.Id;
			this.PageName = model.PageName;
			this.PageViews = model.PageViews;
			this.Sessions = model.Sessions;
			this.UserCount = 0;
			//this.List <ActivityLogViewModel> ActivityLogs = model.xxxxxxxxxxxxxx;
		}

		public KeyMetric Map()
		{
			var model = this.keyMetricRepository.Get(Id);

			if (model == null)
				model = new KeyMetric();

			return this.Map(model);
		}

		public KeyMetric Map(KeyMetric model)
		{
			model.Id = this.Id;
			model.PageName = this.PageName;
			model.PageViews = this.PageViews;
			model.Sessions = this.Sessions;
			//model.Users = this.Users;
			//model.List < ActivityLogViewModel > ActivityLogs = this.xxxxxxxxxxxxxx;

			return model;
		}


		[Key]
		public int Id { get; set; }

		public string PageName { get; set; }

		public int? PageViews { get; set; }

		public string Sessions { get; set; }

		public int UserCount { get; set; }

		public List<ActivityLogViewModel> ActivityLogs { get; set; }
	}
}
