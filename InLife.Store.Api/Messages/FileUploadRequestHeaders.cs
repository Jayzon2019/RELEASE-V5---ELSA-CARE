using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLife.Store.Api.Messages
{
	public class FileUploadRequestHeaders : RequestHeaders
	{
		public string Filename { get; set; }
	}
}
