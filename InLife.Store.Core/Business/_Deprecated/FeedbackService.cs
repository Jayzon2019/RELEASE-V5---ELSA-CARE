using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using Newtonsoft.Json;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Core.Business
{
	public class FeedbackService : IFeedbackService
	{
		private readonly IFeedbackRepository feedbackRepository;

		public FeedbackService
		(
			IFeedbackRepository feedbackRepository
		)
		{
			this.feedbackRepository = feedbackRepository;
		}

		public Feedback SaveFeedback(Feedback feedback)
		{
			Contract.Requires(feedback != null);

			// Save to repository
			this.feedbackRepository.Create(feedback);

			return feedback;
		}
	}
}
