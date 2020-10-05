using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Cms.ViewModels
{
    public class PrimeCareViewModel : BaseContentViewModel
	{
        public string PrimeCareFile { get; set; }

        public string PrimeCareFileName { get; set; }

        public string PrimeCareFileDescription { get; set; }
    }
}
