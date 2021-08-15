using Newtonsoft.Json;

namespace InLife.Store.Api.Messages.External.OrderApi
{
	public class Prospect
	{
		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		[JsonProperty("gender")]
		public string Gender { get; set; }

		[JsonProperty("date_of_birth")]
		public string DateOfBirth { get; set; }

		[JsonProperty("mobile_number")]
		public string MobileNumber { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("prefix_id")]
		public int PrefixId { get; set; }

		[JsonProperty("suffix_id")]
		public int SuffixId { get; set; }

		[JsonProperty("nationality_id")]
		public int NationalityId { get; set; }

		[JsonProperty("civil_status_id")]
		public int CivilStatusId { get; set; }

		[JsonProperty("place_of_birth")]
		public PlaceOfBirth PlaceOfBirth { get; set; }

		[JsonProperty("address")]
		public Address Address { get; set; }
	}
}
