using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Core.Business
{
	public interface IOrderProcessing
	{
		Quote RequestQuote(Quote quote);
	}
}
