using Newtonsoft.Json;

namespace InLife.Store.Api.Messages.External.OrderApi
{
	public class Address
	{
		[JsonProperty("house_number")]
		public int HouseNumber { get; set; }

		[JsonProperty("village")]
		public string Village { get; set; }

		[JsonProperty("street")]
		public string Street { get; set; }

		[JsonProperty("country_id")]
		public int CountryId { get; set; }

		[JsonProperty("province_id")]
		public int ProvinceId { get; set; }

		[JsonProperty("city_id")]
		public int CityId { get; set; }

		[JsonProperty("postal_code")]
		public string PostalCode { get; set; }
	}
}
