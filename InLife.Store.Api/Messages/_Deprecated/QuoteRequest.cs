using System;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Api.Messages
{
	public class QuoteRequest
	{
		// Product Details

		[Required]
		[StringLength(10)]
		public string  ProductCode { get; set; }

		[Required]
		[StringLength(50)]
		public string  ProductName { get; set; }

		[Required]
		public decimal ProductFaceAmount { get; set; }

		[Required]
		public int PaymentMode { get; set; }


		// Customer Details

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


		// Customer Address

		[StringLength(20)]
		[DataType(DataType.PhoneNumber)]
		[Phone]
		public string PhoneNumber { get; set; }

		[Required]
		[StringLength(30)]
		public string Country { get; set; }

		[StringLength(30)]
		public string Region { get; set; }

		[StringLength(30)]
		public string City { get; set; }


		// Others

		[Required]
		[StringLength(80)]
		public string ReferralSource { get; set; }

		[StringLength(50)]
		public string AgentCode { get; set; }

		[StringLength(50)]
		public string AgentFirstName { get; set; }

		[StringLength(50)]
		public string AgentLastName { get; set; }

		// Health

		[Required]
		public bool Health1 { get; set; }

		[Required]
		public bool Health2 { get; set; }

		[Required]
		public bool Health3 { get; set; }

		public bool IsEligible { get; set; }
	}
}
