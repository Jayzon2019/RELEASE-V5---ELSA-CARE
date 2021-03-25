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
		public Sftp GroupSftp { get; set; }
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

	public class Sftp
	{
		public string Host { get; set; }
		public int Port { get; set; } = 22;
		public string Username { get; set; }
		public string Password { get; set; }
		public string PrivateKey { get; set; }
		public string Passphrase { get; set; }
		public string Directory { get; set; }
	}
}
