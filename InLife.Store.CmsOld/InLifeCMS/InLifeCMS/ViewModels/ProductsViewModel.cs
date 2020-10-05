using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLifeCMS.ViewModels
{
    public partial class ProductsViewModel
    {
        [Key]
        public int intProductId { get; set; }
        public string strProductImg { get; set; }
        [Required(ErrorMessage = "Product Name is required!!!")]
        public string strProductName { get; set; }
        [Required(ErrorMessage = "Product Price is required!!!")]
        public string strProductPrice { get; set; }
        
        public string strProductCode { get; set; }

        public string strShortDescription { get; set; }
        public DateTime dteCreatedDate { get; set; }
        public int intCreatedBy { get; set; }

        public string strCreatedByUser { get; set; }

        public string strUpdatedByUser { get; set; }

        public DateTime? dteUpdatedDate { get; set; }
        public int? intUpdatedBy { get; set; }
        public bool blnIsActive { get; set; }
        public bool blnIsArchived { get; set; }
        public string strPriceWithOffer { get; set; }
        [Required(ErrorMessage = "Sort Number is required!!!")]
        public int? intSortNum { get; set; }
    }
}
