using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class UserViewModel
	{
		public UserViewModel()
		{
		}

		public UserViewModel(User model)
		{
			Id = model.Id;
			Email = model.Email;
			FirstName = model.FirstName;
			MiddleName = model.MiddleName;
			LastName = model.LastName;
			IsLocked = (model.LockoutEnd.HasValue && model.LockoutEnd.Value > DateTimeOffset.UtcNow);
		}

		public User Map()
		{
			var model = new User();
			return this.Map(model);
		}

		public User Map(User model)
		{
			model.UserName = Email;
			//model.PhoneNumber = this.Phone;
			model.FirstName = FirstName;
			model.MiddleName = MiddleName;
			model.LastName = LastName;

			if (IsLocked)
				model.LockoutEnd = DateTimeOffset.Now.AddYears(1000);
			else
				model.LockoutEnd = null;

			return model;
		}


		public string Id { get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(256)]
		[DisplayName("Email")]
		public string Email { get; set; }

		//[Required]
		//[Phone]
		//[MaxLength(20)]
		//[DisplayName("Phone")]
		//public string Phone { get; set; }

		[Required]
		[MaxLength(50)]
		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[MaxLength(50)]
		[DisplayName("Middle Name")]
		public string MiddleName { get; set; }

		[Required]
		[MaxLength(50)]
		[DisplayName("Last Name")]
		public string LastName { get; set; }

		public bool IsLocked { get; set; } = false;

		public List<UserRolesViewModel> Roles { get; set; }
	}
}
