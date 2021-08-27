using System;
using Newtonsoft.Json.Linq;
using InLife.Store.Core.Helpers;
using InLife.Store.Resources;
using InLife.Store.Api.Messages.External.OrderApi;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class OrderDetailsResponse
	{
		public string TransactionReferenceId { get; set; }

		public string OrderNumber { get; set; }
		public string OrderItemNumber { get; set; }
		public string OrderStatus { get; set; }
		public DateTime CreatedDate { get; set; }

		public string PlanName { get; set; }
		public string PlanCode { get; set; }
		public decimal PlanPremium { get; set; }
		public string PaymentFrequency { get; set; }

		public string ProspectNamePrefix { get; set; }
		public int? ProspectNamePrefixId { get; set; }
		public string ProspectNameSuffix { get; set; }
		public int? ProspectNameSuffixId { get; set; }
		public string ProspectFirstName { get; set; }
		public string ProspectMiddleName { get; set; }
		public string ProspectLastName { get; set; }

		public string ProspectNationality { get; set; }
		public int? ProspectNationalityId { get; set; }
		public string ProspectCivilStatus { get; set; }
		public int? ProspectCivilStatusId { get; set; }
		public string ProspectGender { get; set; }
		public int? ProspectGenderId { get; set; }
		public DateTime? ProspectBirthDate { get; set; }

		public string ProspectEmailAddress { get; set; }
		public string ProspectMobileNumber { get; set; }
		public string ProspectPhoneeNumber { get; set; }

		public string ProspectBirthCity { get; set; }
		public int? ProspectBirthCityId { get; set; }
		public string ProspectBirthRegion { get; set; }
		public int? ProspectBirthRegionId { get; set; }
		public string ProspectBirthZipCode { get; set; }
		public string ProspectBirthCountry { get; set; }
		public int? ProspectBirthCountryId { get; set; }

		public string ProspectHomeAddress1 { get; set; }
		public string ProspectHomeAddress2 { get; set; }
		public string ProspectHomeAddress3 { get; set; }
		public string ProspectHomeTown { get; set; }
		public int? ProspectHomeTownId { get; set; }
		public string ProspectHomeCity { get; set; }
		public int? ProspectHomeCityId { get; set; }
		public string ProspectHomeRegion { get; set; }
		public int? ProspectHomeRegionId { get; set; }
		public string ProspectHomeZipCode { get; set; }
		public string ProspectHomeCountry { get; set; }
		public int? ProspectHomeCountryId { get; set; }
	}
}
