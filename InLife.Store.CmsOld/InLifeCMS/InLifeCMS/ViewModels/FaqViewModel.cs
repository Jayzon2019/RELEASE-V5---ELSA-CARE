using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLifeCMS.ViewModel
{
    public partial class FaqViewModel
    {
        [Key]
        public int intFaqId { get; set; }
        [Required(ErrorMessage = "Faq Category is required!!!")]
        public int intFaqCatId { get; set; }
        
        public string strFaqCat { get; set; }
        [Required(ErrorMessage = "Faq Question is required!!!")]
        public string strFaqQuestion { get; set; }
        [Required(ErrorMessage = "Faq Answer is required!!!")]
        public string strFaqAnswer { get; set; }
        public string strCreatedByUser { get; set; }
        public string strUpdatedByUser { get; set; }
        public DateTime dteCreatedDate { get; set; }
        public int intCreatedBy { get; set; }
        public DateTime? dteUpdatedDate { get; set; }
        public int? intUpdatedBy { get; set; }
        public bool blnIsActive { get; set; }
        public bool blnIsArchived { get; set; }
        [Required(ErrorMessage = "Sort Number is required!!!")]
        public int? intSortNum { get; set; }
    }
}
