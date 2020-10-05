using System;
using System.Collections.Generic;

namespace InLifeCMS.Models
{
    public partial class TblProducts
    {
        public int ProductId { get; set; }
        public string ProductImg { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public string ShortDescription { get; set; }
        public string PriceWithOffer { get; set; }
        public int? SortNum { get; set; }
    }
}
