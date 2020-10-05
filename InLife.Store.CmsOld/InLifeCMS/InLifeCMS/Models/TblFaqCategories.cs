using System;
using System.Collections.Generic;

namespace InLifeCMS.Models
{
    public partial class TblFaqCategories
    {
        public int FaqCatId { get; set; }
        public string FaqCategory { get; set; }
        public string FaqCatDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
    }
}
