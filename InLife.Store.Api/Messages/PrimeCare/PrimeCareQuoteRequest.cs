using System;
using System.ComponentModel.DataAnnotations;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class PrimeCareQuoteRequest
	{
		public PrimeCareQuoteForm Map(PrimeCareQuoteForm model = null)
		{
			if (model == null)
				model = new PrimeCareQuoteForm();

			model.PlanCode = PlanCode;
			model.PlanName = PlanName;
			model.PlanFaceAmount = PlanFaceAmount;
			model.PlanPremium = PlanPremium;
			model.PaymentFrequency = PaymentFrequency;

			model.NamePrefix = NamePrefix;
			model.NameSuffix = NameSuffix;
			model.FirstName = FirstName;
			model.MiddleName = MiddleName;
			model.LastName = LastName;
			model.Gender = Gender;
			model.BirthDate = BirthDate;
			model.EmailAddress = EmailAddress;
			model.MobileNumber = MobileNumber;

			model.City = City;
			model.Region = Region;
			model.Country = Country;
			model.PhoneNumber = PhoneNumber;

			model.ReferralSource = ReferralSource;
			model.AgentCode = AgentCode;
			model.AgentFirstName = AgentFirstName;
			model.AgentLastName = AgentLastName;

			model.Health1 = Health1;
			model.Health2 = Health2;
			model.Health3 = Health3;

			return model;
		}

		#region Product Details

		[Required]
		[StringLength(30)]
		public string  PlanCode { get; set; }

		[Required]
		[StringLength(30)]
		public string PlanName { get; set; }

		[Required]
		public decimal PlanFaceAmount { get; set; }

		[Required]
		public decimal PlanPremium { get; set; }

		[Required]
		[StringLength(20)]
		public string PaymentFrequency { get; set; }

		#endregion Product Details

		#region Customer Details

		[StringLength(10)]
		public string NamePrefix { get; set; }

		[StringLength(10)]
		public string NameSuffix { get; set; }

		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(50)]
		public string MiddleName { get; set; }

		[Required]
		[StringLength(50)]
		public string LastName { get; set; }

		[Required]
		[StringLength(20)]
		public string Gender { get; set; }

		[Required]
		public DateTime? BirthDate { get; set; }

		[Required]
		[StringLength(300)]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string EmailAddress { get; set; }

		[Required]
		[StringLength(20)]
		[DataType(DataType.PhoneNumber)]
		[Phone]
		public string MobileNumber { get; set; }

		#endregion Customer Details

		#region Customer Address

		[StringLength(20)]
		[DataType(DataType.PhoneNumber)]
		[Phone]
		public string PhoneNumber { get; set; }

		[Required]
		[StringLength(50)]
		public string Country { get; set; }

		[StringLength(50)]
		public string Region { get; set; }

		[StringLength(50)]
		public string City { get; set; }

		#endregion Customer Address

		#region Referral

		[Required]
		[StringLength(80)]
		public string ReferralSource { get; set; }

		[StringLength(50)]
		public string AgentCode { get; set; }

		[StringLength(50)]
		public string AgentFirstName { get; set; }

		[StringLength(50)]
		public string AgentLastName { get; set; }

		#endregion Referral

		#region Health

		[Required]
		public bool Health1 { get; set; }

		[Required]
		public bool Health2 { get; set; }

		[Required]
		public bool Health3 { get; set; }

		#endregion Health
	}
}
