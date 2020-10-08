using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class UserViewModel
	{
		private readonly IUserRepository userRepository;

		public UserViewModel(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public UserViewModel(User model)
		{
			this.Id = model.Id;
			this.UserName = model.UserName;
			this.FirstName = model.FirstName;
			this.MiddleName = model.MiddleName;
			this.LastName = model.LastName;
		}

		public User Map()
		{
			var model = this.userRepository.Get(Id);

			if (model == null)
				model = new User();

			return this.Map(model);
		}

		public User Map(User model)
		{
			model.UserName = this.UserName;
			model.FirstName = this.FirstName;
			model.MiddleName = this.MiddleName;
			model.LastName = this.LastName;

			return model;
		}


		public Guid? Id { get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(256)]
		[DisplayName("Email")]
		public string UserName { get; set; }

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
