using System;
using System.Collections.Generic;

namespace InLifeCMS.Models
{
    public partial class Customers
    {
        public int Id { get; set; }
        public string NamePrefix { get; set; }
        public string NameSuffix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string CivilStatus { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public string BirthRegion { get; set; }
        public string BirthCity { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public int? HomeAddressId { get; set; }
        public int? WorkAddressId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
