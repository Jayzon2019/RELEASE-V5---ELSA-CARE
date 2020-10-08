using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Business;
using InLife.Store.Core.Services;

using InLife.Store.Api.Messages;

namespace InLife.Store.Api
{
	[ApiController]
	[Route("[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	public class OrdersController : BaseController
	{
		public OrdersController
		(
			ILogger<BaseController> logger
		) : base
		(
			logger
		)
		{

		}
	}
}
