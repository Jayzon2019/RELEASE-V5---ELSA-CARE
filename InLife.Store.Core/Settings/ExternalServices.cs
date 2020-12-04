using System;
using System.Collections;
using System.Collections.Generic;

namespace InLife.Store.Core.Settings
{
	public class ExternalServices
	{
		public PrimeCareApi PrimeCareApi { get; set; }
		public AffiliateApi AffiliateApi { get; set; }
		public string PaymentGateway { get; set; }
	}

	public class PrimeCareApi
	{
		public string SubscriptionKey { get; set; }
		public string Host { get; set; }
		public string CreateApplicationEndpoint { get; set; }
		public string SavePaymentEndpoint { get; set; }
	}

	public class AffiliateApi
	{
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string Host { get; set; }
		public string AgentInfoEndpoint { get; set; }
	}
}
