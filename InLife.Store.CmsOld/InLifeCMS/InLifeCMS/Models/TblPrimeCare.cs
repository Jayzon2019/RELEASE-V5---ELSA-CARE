using System;
using System.Collections.Generic;

namespace InLifeCMS.Models
{
    public partial class TblPrimeCare
    {
        public int PrimeCareId { get; set; }
        public string PrimeCareFile { get; set; }
        public string PrimeCareFileDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public string PrimeCareFileName { get; set; }
    }
}
