using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Core.Business
{
	public interface IFeedbackService
	{
		Feedback SaveFeedback(Feedback feedback);
	}
}
