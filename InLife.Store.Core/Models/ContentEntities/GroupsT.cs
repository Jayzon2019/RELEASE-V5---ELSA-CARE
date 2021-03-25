using System;
using System.Collections.Generic;
using System.Text;

namespace InLife.Store.Core.Models
{
	public class GroupsT : BaseContentEntity
	{
		public Guid GroupId { get; set; }
		public int TotalNumberOfMembers { get; set; }
		public string PlanType { get; set; }
		public string CompanyName { get; set; }
		public string CompanyLandLineNo { get; set; }
		public string CompanyMobileNo { get; set; }
		public int StreetNumer { get; set; }
		public string VillageName { get; set; }
		public string Barangaya { get; set; }
		public string Region { get; set; }
		public string City { get; set; }
		public int ZipCode { get; set; }
		public string AuthPrefixName { get; set; }
		public string AuthFristName { get; set; }
		public string AuthMiddleName { get; set; }
		public string AuthLastName { get; set; }
		public string AuthSuffixName { get; set; }
		public string AuthEamilId { get; set; }
		public string AuthMobileNumber { get; set; }
		public string AuthLandlineNo { get; set; }
		public string Status { get; set; }
	}
}
