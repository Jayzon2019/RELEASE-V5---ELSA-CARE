using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLifeCMS.ViewModel
{
    public partial class FaqCategoriesViewModel
    {
        [Key]
        public int intFaqCatId { get; set; }
        [Required(ErrorMessage = "Faq Question is required!!!")]
        public string strFaqCategory { get; set; }
        [Required(ErrorMessage = "Faq Question is required!!!")]
        public string strFaqCatDescription { get; set; }
        public string strCreatedByUser { get; set; }
        public string strUpdatedByUser { get; set; }
        public DateTime dteCreatedDate { get; set; }
        public int intCreatedBy { get; set; }
        public DateTime? dteUpdatedDate { get; set; }
        public int? intUpdatedBy { get; set; }
        public bool blnIsActive { get; set; }
        public bool blnIsArchived { get; set; }
    }
}
