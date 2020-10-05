using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Core.Business
{
	public interface IContentManagement
	{
		Quote RequestQuote(Quote quote);
	}
}
