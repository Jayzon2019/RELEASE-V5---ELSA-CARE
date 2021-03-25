using InLife.Store.Core.Business;
using InLife.Store.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using InLife.Store.Api.Messages;
using InLife.Store.Core.Repository.Modals;
using InLife.Store.Core.Models.ContentEntities;

namespace InLife.Store.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[Produces(MediaTypeNames.Application.Json)]
	public class ApplyDocumentsController : BaseController
	{
		private readonly IApplyDocumentsRepository applyDocumentsRepository;
		private readonly IApplyDocumentService applyDocumentService;
		public ApplyDocumentsController
		(
			ILogger<BaseController> logger,
			IApplyDocumentsRepository applyDocumentsRepository,
			IApplyDocumentService applyDocumentService
		) : base
		(
			logger
		)
		{
			this.applyDocumentsRepository = applyDocumentsRepository;
			this.applyDocumentService = applyDocumentService;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		[Route("create")]
		public ActionResult Create([FromBody] ApplyDocumentsRequest applyDocumentsRequest)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{

				var applyDocumentsResquest = new ApplyDocuments
				{
					SECRegistration = applyDocumentsRequest.SECRegistration,
					EmployeeCesusForm= applyDocumentsRequest.EmployeeCesusForm,
					EntityPlanForm = applyDocumentsRequest.EntityPlanForm,
					AuthRepresentativeID = applyDocumentsRequest.AuthRepresentativeID,
					BIRNoticeForm = applyDocumentsRequest.BIRNoticeForm,
					IncorporationArticles = applyDocumentsRequest.IncorporationArticles,
					IdentityCertificate = applyDocumentsRequest.IdentityCertificate,
					PostPolicyForm = applyDocumentsRequest.PostPolicyForm,
					IsCheckDataPrivacy = applyDocumentsRequest.IsCheckDataPrivacy,
					IsCheckUNSCR = applyDocumentsRequest.IsCheckUNSCR,
					IsCheckDeclarationStatement = applyDocumentsRequest.IsCheckDeclarationStatement,
					IsCheckSubmittedPhlippinesApp = applyDocumentsRequest.IsCheckSubmittedPhlippinesApp,
					IsCheckInLifeProducts = applyDocumentsRequest.IsCheckInLifeProducts
				};

				this.applyDocumentService.SaveApplyDocuments(applyDocumentsResquest);

				var response = new ApplyDocumentsRequest
				{
					ApplyDocumentsId = applyDocumentsRequest.ApplyDocumentsId
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
