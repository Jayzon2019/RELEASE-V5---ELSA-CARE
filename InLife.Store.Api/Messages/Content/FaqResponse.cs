using System;

namespace InLife.Store.Api.Messages
{
	public class FaqResponse
	{
		public int Id { get; set; }

		public int CategoryId { get; set; }

		public string CategoryName { get; set; }

		public string Question { get; set; }

		public string Answer { get; set; }

		public int SortNum { get; set; } = 1000;
	}
}
