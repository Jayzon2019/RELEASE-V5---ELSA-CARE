using Newtonsoft.Json;

namespace InLife.Store.Api.Messages.External.OrderApi
{
	public class Order
	{
		[JsonProperty("order_number")]
		public string OrderNumber { get; set; }

		[JsonProperty("order_item_number")]
		public string OrderItemNumber { get; set; }

		[JsonProperty("plan_name")]
		public string PlanName { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("created_on")]
		public string CreatedOn { get; set; }

		[JsonProperty("premium")]
		public Premium Premium { get; set; }

		[JsonProperty("prospect")]
		public Prospect Prospect { get; set; }
	}
}
