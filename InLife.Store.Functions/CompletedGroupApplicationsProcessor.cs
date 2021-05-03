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

		// Every CRON schedule has been adjusted from UTC to Manila Time
		// Dev test value = Daily @ 12AM                  "0 0 16 * * *"
		// Dev test value = Every 1 hour                  "0 0 * * * *"
		// Dev test value = Every 12AM, 6AM, 12PM, 6PM    "0 0 4,10,16,22 * * *"
		// Prod values    = 6AM and 12PM                  "0 0 4,22 * * *"
		[FunctionName("CompletedGroupApplicationsProcessor")]
		public async Task Run([TimerTrigger("0 0 4,10,16,22 * * *")] TimerInfo timer, ILogger log)
		{
			await applicationProcessing.ProcessCompletedApplications();
			log.LogInformation($"Processed completed group applications: {DateTime.Now}");
		}
	}
}
