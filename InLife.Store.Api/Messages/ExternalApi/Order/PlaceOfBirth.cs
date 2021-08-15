using Newtonsoft.Json;

namespace InLife.Store.Api.Messages.External.OrderApi
{
	public class PlaceOfBirth
	{
		[JsonProperty("country_id")]
		public int CountryId { get; set; }

		[JsonProperty("province_id")]
		public int ProvinceId { get; set; }

		[JsonProperty("city_id")]
		public int CityId { get; set; }
	}
}
