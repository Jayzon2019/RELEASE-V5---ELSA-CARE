using System;
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
			this.Id = model.Id;
			this.Email = model.Email;
			this.FirstName = model.FirstName;
			this.MiddleName = model.MiddleName;
			this.LastName = model.LastName;
		}

		public User Map()
		{
			var model = new User();
			return this.Map(model);
		}

		public User Map(User model)
		{
			model.UserName = this.Email;
			//model.PhoneNumber = this.Phone;
			model.FirstName = this.FirstName;
			model.MiddleName = this.MiddleName;
			model.LastName = this.LastName;

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
	}
}
