using System;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Api.Messages
{
	public class FileUploadRequest
	{
		[Required]
		public string DataUri { get; set; }
	}
}
