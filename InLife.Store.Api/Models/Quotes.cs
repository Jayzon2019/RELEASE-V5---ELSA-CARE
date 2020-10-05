using System;
using System.Collections.Generic;

namespace InLife.Store.Api.Models
{
    public partial class Quotes
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductFaceAmount { get; set; }
        public int? PaymentMode { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentLastName { get; set; }
        public string ReferralSource { get; set; }
        public bool? Health1 { get; set; }
        public bool? Health2 { get; set; }
        public bool? Health3 { get; set; }
        public bool? IsEligible { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
