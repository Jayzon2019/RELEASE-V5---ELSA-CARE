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
	public class OrderProcessing : IOrderProcessing
	{
		private readonly ICustomerRepository customerRepository;
		private readonly IQuoteRepository quoteRepository;
		//private readonly IOrderRepository orderRepository;
		//private readonly IUserRepository userRepository;

		public OrderProcessing
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

			// Validate for eligibility
		//	quote.IsEligible =
		//		quote.HealthCondition1
		//		|| quote.HealthCondition2
		//		|| quote.HealthCondition3
		//		|| quote.

		//		if ((healthCondition.healthCondition1 === 'Yes' || healthCondition.healthCondition2 === 'Yes' ||

		//		(this.plan === 'plan_30' && this.calulatedAge > 59) ||
		//		(this.plan !== 'plan_30' && this.calulatedAge > 50) ||

		//		healthCondition.healthCondition3 === 'Yes') || this.getQuoteForm.value.basicInformation.country !== '196'
		//|| parseFloat(this.amount.toString().replace(/,/ g, '')) >= parseFloat(("50,000").replace(/,/ g, '')))
		//	{
		//		// this.router.navigate(['Ineligible']);
		//		location.href = "ineligible.html";
		//	}
		//	else
		//	{
		//		this.router.navigate(['apply']);

		//	}



			// Save to repository
			this.quoteRepository.Create(quote);

			return quote;
		}
	}
}
