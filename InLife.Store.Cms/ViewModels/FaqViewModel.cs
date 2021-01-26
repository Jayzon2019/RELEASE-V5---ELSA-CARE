using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class FaqViewModel : BaseContentViewModel
	{
		public FaqViewModel()
		{
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
			var model = new Faq();
			return this.Map(model);
		}

		public Faq Map(Faq model)
		{
			model.Question = this.Question;
			model.Answer = this.Answer;
			model.SortNum = this.SortNum;

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
		[DisplayName("Answer")]
		public string Answer { get; set; }

		[DisplayName("Sort Index")]
		public int? SortNum { get; set; } = 1000;
	}
}
