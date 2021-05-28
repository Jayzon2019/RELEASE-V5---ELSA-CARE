using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class PrimeCareQuoteResponse : BasePrimeCareResponse
	{
		public PrimeCareQuoteResponse() : base()
		{
		}

		public PrimeCareQuoteResponse(PrimeCareApplication model) : base(model)
		{
		}

		public bool IsEligible { get; set; }
	}
}
