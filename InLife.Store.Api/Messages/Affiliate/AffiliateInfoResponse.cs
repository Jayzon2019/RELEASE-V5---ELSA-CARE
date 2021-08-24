using System;

namespace InLife.Store.Api.Messages
{
	public class AffiliateInfoResponse
	{
		public Agent? Agent { get; set; }

		public Affiliate? Affiliate { get; set; }

		public Result Result { get; set; }

		public string AffiliateType { get; set; }
	}
}
