using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Cms.ViewModels
{
	public class ErrorViewModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
