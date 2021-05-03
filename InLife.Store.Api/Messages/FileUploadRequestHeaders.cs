using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InLife.Store.Api.Messages
{
	public class FileUploadRequestHeaders : RequestHeaders
	{
		[FromHeader]
		public string Filename { get; set; }
	}
}
