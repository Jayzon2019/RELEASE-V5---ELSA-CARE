using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using InLife.Store.Core.Models;
using InLife.Store.Core.Settings;
using InLife.Store.Core.Services;
using Renci.SshNet;
using Renci.SshNet.Async;
using Renci.SshNet.Sftp;
using MimeKit;
using ClosedXML.Excel;

namespace InLife.Store.Infrastructure.Services
{
	public class SftpService : ISftpService
	{
		private readonly IHostEnvironment hostingEnvironment;
		private readonly ExternalServices settings;

		public SftpService
		(
			IHostEnvironment hostingEnvironment,
			IOptions<ExternalServices> settings
		)
		{
			this.hostingEnvironment = hostingEnvironment;
			this.settings = settings.Value;
		}

		#region Business - Group

		public async Task UploadGroupFile(string directory, string filename, Stream stream)
		{
			var targetDirectory = ("/" + settings.GroupSftp.Directory + directory + "/").Replace("//", "/");
			var filePath = targetDirectory + filename;

			var connectionInfo = new ConnectionInfo
			(
				settings.GroupSftp.Host,
				settings.GroupSftp.Username,
				new PrivateKeyAuthenticationMethod(settings.GroupSftp.Username, new PrivateKeyFile(settings.GroupSftp.PrivateKey, settings.GroupSftp.Passphrase))
			);

			using var client = new SftpClient(connectionInfo);
			client.Connect();
			if (client.IsConnected)
			{
				if (!client.Exists(targetDirectory))
					client.CreateDirectory(targetDirectory);

				//TODO: Create a custom delete method since wildcards are not supported by this library
				//client.DeleteFile(Path.GetFileNameWithoutExtension(filename) + ".*");

				await client.UploadAsync(stream, filePath);

				client.Disconnect();
			}
		}

		public async Task<string> UploadGroupFile(GroupApplication application, string documentType, string contentType, Stream stream)
		{
			var companyName = application.CompanyName.Replace("  ", " ");
			foreach (char c in Path.GetInvalidFileNameChars())
				companyName = companyName.Replace(c, Char.MinValue);

			var directory = $"{application.ReferenceCode} - {companyName}";
			var filename = documentType + "." + MediaType.FromId(contentType).Extension;

			var targetDirectory = ("/" + settings.GroupSftp.Directory + directory + "/").Replace("//", "/");
			var filePath = targetDirectory + filename;

			var connectionInfo = new ConnectionInfo
			(
				settings.GroupSftp.Host,
				settings.GroupSftp.Username,
				new PrivateKeyAuthenticationMethod(settings.GroupSftp.Username, new PrivateKeyFile(settings.GroupSftp.PrivateKey, settings.GroupSftp.Passphrase))
			);

			using var client = new SftpClient(connectionInfo);
			client.Connect();
			if (client.IsConnected)
			{
				if (!client.Exists(targetDirectory))
					client.CreateDirectory(targetDirectory);

				//TODO: Create a custom delete method since wildcards are not supported by this library
				//client.DeleteFile(Path.GetFileNameWithoutExtension(filename) + ".*");

				await client.UploadAsync(stream, filePath);

				client.Disconnect();
			}

			return filename;
		}

		public async Task UploadGroupApplicationData(GroupApplication application)
		{
			//var targetDirectory = ("/" + settings.GroupSftp.Directory + directory + "/").Replace("//", "/");
			//var filePath = targetDirectory + filename;

			//var connectionInfo = new ConnectionInfo
			//(
			//	settings.GroupSftp.Host,
			//	settings.GroupSftp.Username,
			//	new PrivateKeyAuthenticationMethod(settings.GroupSftp.Username, new PrivateKeyFile(settings.GroupSftp.PrivateKey, settings.GroupSftp.Passphrase))
			//);

			//using var client = new SftpClient(connectionInfo);
			//client.Connect();
			//if (client.IsConnected)
			//{
			//	//if (!client.Exists(targetDirectory))
			//	//	client.CreateDirectory(targetDirectory);

			//	//TODO: Create a custom delete method since wildcards are not supported by this library
			//	//client.DeleteFile(Path.GetFileNameWithoutExtension(filename) + ".*");

			//	//await client.UploadAsync(stream, filePath);
			//	await Task.Delay(0);
			//}

			await Task.Delay(0);
		}

		public async Task UploadGroupApplicationsBatchData(IEnumerable<GroupApplication> applications)
		{
			var columns = new string[]
			{
				"ReferenceCode",
				"CreatedDate", "CompletedDate", "ExportedDate",

				"ProductCode", "ProductName", "PlanCode", "PlanVariantCode",
				"PlanFaceAmount", "PlanPremium", "PaymentMode", "PaymentFrequency",
				"TotalMembers", "TotalTeachers", "TotalStudents",

				"BusinessStructure", "CompanyName", "CompanyPhoneNumber",
				"CompanyMobileNumber", "CompanyEmailAddress",

				"CompanyAddress1", "CompanyAddress2", "CompanyTown",
				"CompanyCity", "CompanyRegion", "CompanyZipCode", "CompanyCountry",

				"RepresentativeNamePrefix", "RepresentativeNameSuffix",
				"RepresentativeFirstName", "RepresentativeMiddleName", "RepresentativeLastName",
				"RepresentativePhoneNumber", "RepresentativeMobileNumber", "RepresentativeEmailAddress",

				"FeedbackRating", "FeedbackMessage",
				"CancellationReason", "CancellationComments"
			};

			var currentDate = DateTimeOffset.Now.ToString("yyyy-MM-dd-HHmm");
			var directory = $"_BatchUploads";
			var filename = currentDate + ".csv";

			var targetDirectory = ("/" + settings.GroupSftp.Directory + directory + "/").Replace("//", "/");
			var filePath = targetDirectory + filename;

			using var stream = new MemoryStream();
			using var workbook = new XLWorkbook();

			var worksheet = workbook.Worksheets.Add(currentDate);

			// Headers
			for (var i = 1; i <= columns.Length; i++)
			{
				worksheet.Cell(1, i).Value = columns[i - 1];
				worksheet.Cell(1, i).Style.Fill.BackgroundColor = XLColor.FromArgb(64,64,64);
				worksheet.Cell(1, i).Style.Font.FontColor = XLColor.White;
			}

			// Body
			var irow = 2;
			foreach (var application in applications)
			{
				for (var icol = 1; icol <= columns.Length; icol++)
				{
					var value = application.GetType().GetProperty(columns[icol - 1]).GetValue(application, null);
					worksheet.Cell(irow, icol).Value = value.ToString();
					worksheet.Cell(irow, icol).DataType = XLDataType.Text;
				}
				irow++;
			}

			workbook.SaveAs(stream);
			stream.Position = 0;

			var connectionInfo = new ConnectionInfo
			(
				settings.GroupSftp.Host,
				settings.GroupSftp.Username,
				new PrivateKeyAuthenticationMethod(settings.GroupSftp.Username, new PrivateKeyFile(settings.GroupSftp.PrivateKey, settings.GroupSftp.Passphrase))
			);

			using var client = new SftpClient(connectionInfo);
			client.Connect();
			if (client.IsConnected)
			{
				if (!client.Exists(targetDirectory))
					client.CreateDirectory(targetDirectory);

				await client.UploadAsync(stream, filePath);
			}
		}

		#endregion
	}
}
