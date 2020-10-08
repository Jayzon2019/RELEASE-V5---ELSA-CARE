using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class FaqViewModel : BaseContentViewModel
	{
		private readonly IFaqRepository faqRepository;
		private readonly IFaqCategoryRepository faqCategoryRepository;

		public FaqViewModel
		(
			IFaqRepository faqRepository,
			IFaqCategoryRepository faqCategoryRepository
		)
		{
			this.faqRepository = faqRepository;
			this.faqCategoryRepository = faqCategoryRepository;
		}

		public FaqViewModel(Faq model) : base(model)
		{
			this.CategoryId = model.Category.Id;
			this.CategoryName = model.Category.Name;
			this.Question = model.Question;
			this.Answer = model.Answer;
			this.SortNum = model.SortNum;
	}

		public Faq Map()
		{
			var model = this.faqRepository.Get(Id);

			if (model == null)
				model = new Faq();

			return this.Map(model);
		}

		public Faq Map(Faq model)
		{
			var faqCategory = faqCategoryRepository.Get(this.CategoryId);

			model.Category = faqCategory;
			model.Question = this.Question;
			model.Answer = this.Answer;

			return model;
		}

		[Required]
		public int CategoryId { get; set; }

		public string CategoryName { get; set; }

		[Required]
		[MaxLength(300)]
		[DisplayName("Question")]
		public string Question { get; set; }

		[Required]
		[MaxLength(800)]
		[DisplayName("Answer")]
		public string Answer { get; set; }

		[DisplayName("Sort Index")]
		public int? SortNum { get; set; } = 1000;
	}
}
