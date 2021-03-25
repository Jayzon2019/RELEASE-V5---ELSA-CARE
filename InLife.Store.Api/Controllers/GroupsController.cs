using Microsoft.AspNetCore.Http;
using InLife.Store.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.Extensions.Logging;
using InLife.Store.Core.Models;
using InLife.Store.Core.Business;
using InLife.Store.Core.Services;
using InLife.Store.Api.Messages;


namespace InLife.Store.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	public class GroupsController : BaseController
	{
		private readonly IGroupRepository groupRepository;
		private readonly IGroupsService groupsService;
		public GroupsController
		(
			ILogger<BaseController> logger,
			IGroupRepository groupRepository,
			IGroupsService groupsService
		) : base
		(
			logger
		)
		{
			this.groupRepository = groupRepository;
			this.groupsService = groupsService;
		}

		[HttpGet]
		[Route("GetGroupList")]
		public IActionResult GetGroupList()
		{
			try
			{
				var result = groupRepository
					.GetAll()
					.Select(model => new GroupsResponse
					{
						TotalNumberOfMembers = model.TotalNumberOfMembers,
						PlanType = model.PlanType,
						CompanyName = model.CompanyName
					})
					.ToList();

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		[Route("create")]
		public ActionResult Create([FromBody] GroupsResponse groupsResponse)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{

				var groupsT = new GroupsT
				{
					TotalNumberOfMembers = groupsResponse.TotalNumberOfMembers,
					PlanType = groupsResponse.PlanType,
					CompanyName = groupsResponse.CompanyName,
					CompanyLandLineNo = groupsResponse.CompanyLandLineNo,
					CompanyMobileNo = groupsResponse.CompanyMobileNo,
					StreetNumer = groupsResponse.StreetNumer,
					VillageName = groupsResponse.VillageName,
					Barangaya = groupsResponse.Barangaya,
					Region = groupsResponse.Region,
					City = groupsResponse.City,
					ZipCode = groupsResponse.ZipCode,
					AuthPrefixName = groupsResponse.AuthPrefixName,
					AuthFristName = groupsResponse.AuthFristName,
					AuthMiddleName = groupsResponse.AuthMiddleName,
					AuthLastName = groupsResponse.AuthLastName,
					AuthSuffixName = groupsResponse.AuthSuffixName,
					AuthEamilId = groupsResponse.AuthEamilId,
					AuthMobileNumber = groupsResponse.AuthMobileNumber,
					AuthLandlineNo = groupsResponse.AuthLandlineNo,
					Status = groupsResponse.Status,
					
				};

				this.groupsService.SaveGroups(groupsT);

				var response = new GroupsT
				{
					GroupId = groupsT.GroupId
				};

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}
	}

}
