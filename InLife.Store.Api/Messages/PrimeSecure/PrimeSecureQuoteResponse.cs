using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class PrimeSecureQuoteResponse : BasePrimeSecureResponse
	{
		public PrimeSecureQuoteResponse() : base()
		{
		}

		public PrimeSecureQuoteResponse(PrimeSecureApplication model) : base(model)
		{
			//Session = model.Session;
		}

		///public string Session { get; set; }
	}
}
