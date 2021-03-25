using System;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Business;
using InLife.Store.Core.Services;
using InLife.Store.Core.Repository;
using InLife.Store.Core.Utilities;

namespace InLife.Store.Functions
{
	public class CompletedGroupApplicationsProcessor
	{
		private readonly IGroupApplicationProcessing applicationProcessing;

		public CompletedGroupApplicationsProcessor(IGroupApplicationProcessing applicationProcessing)
		{
			this.applicationProcessing = applicationProcessing;
		}

		// Default value = Daily @ 12AM Manila Time "0 0 16 * * *"
		// Dev Test value = Every 1 hour "0 0 * * * *"
		[FunctionName("CompletedGroupApplicationsProcessor")]
		public async Task Run([TimerTrigger("0 0 * * * *")] TimerInfo timer, ILogger log)
		{
			await applicationProcessing.ProcessCompletedApplications();
			log.LogInformation($"Processed completed group applications: {DateTime.Now}");
		}
	}
}
