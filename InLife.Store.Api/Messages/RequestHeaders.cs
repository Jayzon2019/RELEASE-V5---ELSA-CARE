using System;
using Microsoft.AspNetCore.Mvc;

namespace InLife.Store.Api.Messages
{
	public class RequestHeaders
	{

		[FromHeader]
		public string Session { get; set; }
	}
}
