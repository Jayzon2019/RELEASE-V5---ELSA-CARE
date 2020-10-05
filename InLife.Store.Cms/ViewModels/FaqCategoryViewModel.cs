using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;

namespace InLife.Store.Cms.ViewModels
{
	public class FaqCategoryViewModel : BaseContentViewModel
	{
		public FaqCategoryViewModel()
		{
		}

		public FaqCategoryViewModel(FaqCategory model) : base (model)
		{
			this.Name = model.Name;
			this.Description = model.Description;
		}

		public FaqCategory Map()
		{
			var model = new FaqCategory();
			return this.Map(model);
		}

		public FaqCategory Map(FaqCategory model)
		{
			model.Name = this.Name;
			model.Description = this.Description;

			return model;
		}


		[Required]
		[MaxLength(30)]
		[DisplayName("Name")]
		public string Name { get; set; }

		[Required]
		[MaxLength(150)]
		[DisplayName("Description")]
		public string Description { get; set; }
	}
}
