using InLife.Store.Core.Models;
using InLife.Store.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace InLife.Store.Api.Messages.External.OrderApi
{
	public class ExternalOrderResponse
	{
		public ExternalOrderForm ToExternalOrderForm()
		{
			var quoteForm = new ExternalOrderForm
			{
				ReferenceCode = TransactionReferenceId,

				OrderNumber = Order.OrderNumber,
				OrderItemNumber = Order.OrderItemNumber,
				OrderStatus = Order.Status,

				PlanCode = Order.PlanName,
				PlanName = Order.PlanName,
				PlanFaceAmount = Order.Premium.Amount,
				PlanPremium = Order.Premium.Amount,
				PaymentFrequency = Order.Premium.Mode
			};

			quoteForm.NamePrefix = GetReferenceData(ReferenceTables.NamePrefix, Order.Prospect.PrefixId);
			quoteForm.NameSuffix = GetReferenceData(ReferenceTables.NameSuffix, Order.Prospect.SuffixId);
			quoteForm.FirstName = Order.Prospect.FirstName;
			quoteForm.MiddleName = null;
			quoteForm.LastName = Order.Prospect.LastName;
			quoteForm.Gender = Order.Prospect.Gender;

			quoteForm.EmailAddress = Order.Prospect.Email;
			quoteForm.MobileNumber = Order.Prospect.MobileNumber;

			if (DateTime.TryParse(Order.Prospect.DateOfBirth, out DateTime result))
				quoteForm.BirthDate = result;

			quoteForm.BirthCity = GetReferenceData(ReferenceTables.Region, $"$.[*].City.[?(@.id=='{Order.Prospect.PlaceOfBirth?.CityId}')].name");
			quoteForm.BirthRegion = GetReferenceData(ReferenceTables.Region, Order.Prospect.PlaceOfBirth?.ProvinceId);
			quoteForm.BirthZipCode = null;
			quoteForm.BirthCountry = GetReferenceData(ReferenceTables.Country, Order.Prospect.PlaceOfBirth?.CountryId);

			quoteForm.HomeAddress1 = $"{Order.Prospect.Address?.HouseNumber} {Order.Prospect.Address?.Street} {Order.Prospect.Address?.Village}";
			quoteForm.HomeAddress2 = null;
			quoteForm.HomeCity = GetReferenceData(ReferenceTables.Region, $"$.[*].City.[?(@.id=='{Order.Prospect.Address?.CityId}')].name");
			quoteForm.HomeRegion = GetReferenceData(ReferenceTables.Region, Order.Prospect.Address?.ProvinceId);
			quoteForm.HomeZipCode = Order.Prospect.Address?.PostalCode;
			quoteForm.HomeCountry = GetReferenceData(ReferenceTables.Country, Order.Prospect.Address?.CountryId);

			return quoteForm;
		}


		public OrderDetailsResponse ToOrderDetailResponse()
		{
			var response = new OrderDetailsResponse();

			response.TransactionReferenceId = TransactionReferenceId;

			response.OrderNumber = Order?.OrderNumber;
			response.OrderItemNumber = Order.OrderItemNumber;
			response.OrderStatus = Order?.Status;

			response.CreatedDate = DateTime.TryParse(Order?.CreatedOn, out DateTime result)
				? result
				: DateTime.UtcNow;

			response.PlanName = Order?.PlanName;
			response.PlanCode = "";
			response.PlanPremium = (decimal)Order?.Premium?.Amount;
			response.PaymentFrequency = Order?.Premium?.Mode;

			response.ProspectNamePrefix = GetReferenceData(ReferenceTables.NamePrefix, Order?.Prospect?.PrefixId);
			response.ProspectNamePrefixId = Order?.Prospect?.PrefixId;
			response.ProspectNameSuffix = GetReferenceData(ReferenceTables.NameSuffix, Order?.Prospect?.SuffixId);
			response.ProspectNameSuffixId = Order?.Prospect?.SuffixId;

			response.ProspectFirstName = Order?.Prospect.FirstName;
			response.ProspectMiddleName = null;
			response.ProspectLastName = Order?.Prospect.LastName;

			response.ProspectNationality = GetReferenceData(ReferenceTables.Country, Order?.Prospect?.NationalityId);
			response.ProspectNationalityId = Order?.Prospect?.NationalityId;
			response.ProspectCivilStatus = GetReferenceData(ReferenceTables.CivilStatus, Order?.Prospect?.CivilStatusId);
			response.ProspectCivilStatusId = Order?.Prospect?.CivilStatusId;
			response.ProspectGender = Order?.Prospect?.Gender;

			response.ProspectEmailAddress = Order?.Prospect?.Email;
			response.ProspectMobileNumber = Order?.Prospect?.MobileNumber;
			response.ProspectPhoneeNumber = "";

			if (DateTime.TryParse(Order?.Prospect?.DateOfBirth, out result))
				response.ProspectBirthDate = result;

			response.ProspectBirthCity = GetReferenceData(ReferenceTables.Region, $"$.[*].City.[?(@.id=='{Order?.Prospect?.PlaceOfBirth?.CityId}')].name");
			response.ProspectBirthCityId = Order?.Prospect?.PlaceOfBirth?.CityId;
			response.ProspectBirthRegion = GetReferenceData(ReferenceTables.Region, Order?.Prospect?.PlaceOfBirth?.ProvinceId);
			response.ProspectBirthRegionId = Order?.Prospect?.PlaceOfBirth?.ProvinceId;
			response.ProspectBirthZipCode = null;
			response.ProspectBirthCountry = GetReferenceData(ReferenceTables.Country, Order?.Prospect?.PlaceOfBirth?.CountryId);

			response.ProspectHomeAddress1 = $"{Order?.Prospect?.Address?.HouseNumber}";
			response.ProspectHomeAddress2 = $"{Order?.Prospect?.Address?.Street}";
			response.ProspectHomeAddress3 = $"{Order?.Prospect?.Address?.Village}";
			response.ProspectHomeTown = null;
			response.ProspectHomeCity = GetReferenceData(ReferenceTables.Region, $"$.[*].City.[?(@.id=='{Order?.Prospect?.Address?.CityId}')].name");
			response.ProspectHomeCityId = Order?.Prospect?.Address?.CityId;
			response.ProspectHomeRegion = GetReferenceData(ReferenceTables.Region, Order?.Prospect?.Address?.ProvinceId);
			response.ProspectHomeRegionId = Order?.Prospect?.Address?.CountryId;
			response.ProspectHomeZipCode = Order?.Prospect?.Address?.PostalCode;
			response.ProspectHomeCountry = GetReferenceData(ReferenceTables.Country, Order?.Prospect?.Address?.CountryId);
			response.ProspectHomeCountryId = Order?.Prospect?.Address?.CountryId;

			return response;
		}

		[JsonProperty("transaction_reference_id")]
		public string TransactionReferenceId { get; set; }

		[JsonProperty("order")]
		public Order Order { get; set; }


		// TODO: Move this to helper class
		private string GetReferenceData(string refTable, object id)
		{
			return JArray
				.Parse(refTable)
				.SelectToken($"$.[?(@.id=='{id}')].name")
				.ToString();
		}

		private string GetReferenceData(string refTable, string jsonpath)
		{
			return JArray
				.Parse(refTable)
				.SelectToken(jsonpath)
				.ToString();
		}
	}
}
