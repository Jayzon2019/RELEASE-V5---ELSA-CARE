using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class FaqCategoryViewModel : BaseContentViewModel
	{
		private readonly IFaqCategoryRepository faqCategoryRepository;

		public FaqCategoryViewModel(IFaqCategoryRepository faqCategoryRepository)
		{
			this.faqCategoryRepository = faqCategoryRepository;
		}

		public FaqCategoryViewModel(FaqCategory model) : base (model)
		{
			this.Name = model.Name;
			this.Description = model.Description;
		}

		public FaqCategory Map()
		{
			var model = this.faqCategoryRepository.Get(Id);

			if (model == null)
				model = new FaqCategory();

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
