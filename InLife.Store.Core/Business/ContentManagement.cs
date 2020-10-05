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
	public class ContentManagement : IContentManagement
	{
		private readonly ICustomerRepository customerRepository;
		private readonly IQuoteRepository quoteRepository;

		public ContentManagement
		(
			ICustomerRepository customerRepository,
			IQuoteRepository quoteRepository
		)
		{
			this.customerRepository = customerRepository;
			this.quoteRepository = quoteRepository;
		}

		public Quote RequestQuote(Quote quote)
		{
			Contract.Requires(quote != null);



			// Save to repository
			this.quoteRepository.Create(quote);

			return quote;
		}
	}
}
