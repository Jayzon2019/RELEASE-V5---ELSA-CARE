using System;

namespace InLife.Store.Api.Messages
{
	public class PrimeCareQuoteResponse
	{
		public string ReferenceCode { get; set; }

		public string Status { get; set; }

		public bool IsEligible { get; set; }
	}
}
