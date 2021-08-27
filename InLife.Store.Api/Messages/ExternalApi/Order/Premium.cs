using Newtonsoft.Json;

namespace InLife.Store.Api.Messages.External.OrderApi
{
	public class Premium
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("mode")]
		public string Mode { get; set; }

		[JsonProperty("amount")]
		public decimal Amount { get; set; }
	}
}
