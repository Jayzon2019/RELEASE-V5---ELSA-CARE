using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class PrimeCareViewModel : BaseContentViewModel
	{
		public PrimeCareViewModel()
		{
		}

		public PrimeCareViewModel(PrimeCare model) : base(model)
		{
			this.PrimeCareFile = model.PrimeCareFile;
			this.PrimeCareFileName = model.PrimeCareFileName;
			this.PrimeCareFileDescription = model.PrimeCareFileDescription;
		}

		public PrimeCare Map()
		{
			var model = new PrimeCare();
			return this.Map(model);
		}

		public PrimeCare Map(PrimeCare model)
		{
			model.PrimeCareFile = this.PrimeCareFile;
			model.PrimeCareFileName = this.PrimeCareFileName;
			model.PrimeCareFileDescription = this.PrimeCareFileDescription;

			return model;
		}


		public string PrimeCareFile { get; set; }

		public string PrimeCareFileName { get; set; }

		public string PrimeCareFileDescription { get; set; }
	}
}
