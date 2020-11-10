using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public partial class UserRolesViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public bool Selected { get; set; }
	}
}
