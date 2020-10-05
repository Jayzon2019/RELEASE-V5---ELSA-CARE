using System;
using System.Collections.Generic;

namespace InLife.Store.Api.Models
{
    public partial class TblFaq
    {
        public int FaqId { get; set; }
        public int FaqCatId { get; set; }
        public string FaqQuestion { get; set; }
        public string FaqAnswer { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public int? SortNum { get; set; }
    }
}
