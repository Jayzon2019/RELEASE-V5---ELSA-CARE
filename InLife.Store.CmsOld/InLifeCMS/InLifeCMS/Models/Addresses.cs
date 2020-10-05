using System;
using System.Collections.Generic;

namespace InLifeCMS.Models
{
    public partial class Addresses
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
