using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

using InLife.Store.Core.Models;

namespace InLife.Store.Api.Helpers
{
	public static class Comman
	{

		private static IHttpContextAccessor httpContextAccessor;
		public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
		{
			httpContextAccessor = accessor;
		}

		public static string SomethingWntWrong = "Something went wrong please try again later!!!";
		public static string Encrypt(string clearText)
		{
			try
			{
				byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
				using (Aes encryptor = Aes.Create())
				{
					string EncryptionKey = "MAKV2SPBNI99212";
					Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
					encryptor.Key = pdb.GetBytes(32);
					encryptor.IV = pdb.GetBytes(16);
					using (MemoryStream ms = new MemoryStream())
					{
						using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
						{
							cs.Write(clearBytes, 0, clearBytes.Length);
							cs.Close();
						}
						clearText = Convert.ToBase64String(ms.ToArray());
					}
				}
				return clearText;
			}
			catch (Exception ex)
			{

				return ex.Message;
			}
		}

		public static string Decrypt(string cipherText)
		{
			try
			{
				string EncryptionKey = "MAKV2SPBNI99212";
				byte[] cipherBytes = Convert.FromBase64String(cipherText);
				using (Aes encryptor = Aes.Create())
				{
					Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
					encryptor.Key = pdb.GetBytes(32);
					encryptor.IV = pdb.GetBytes(16);
					using (MemoryStream ms = new MemoryStream())
					{
						using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
						{
							cs.Write(cipherBytes, 0, cipherBytes.Length);
							cs.Close();
						}
						cipherText = Encoding.Unicode.GetString(ms.ToArray());
					}
				}
				return cipherText;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		public static string GetRandamGuid()
		{
			Guid g = Guid.NewGuid();
			string GuidString = Convert.ToBase64String(g.ToByteArray());
			GuidString = GuidString.Replace("=", "");
			GuidString = GuidString.Replace("+", "");
			return GuidString;
		}

		public static string ActivationEmailBody(string Guid, int id)
		{
			var url = httpContextAccessor.HttpContext.Request.Scheme + "://" + httpContextAccessor.HttpContext.Request.Host + "/Home/ActivateAccount?uid=" + id + "&token=" + Guid;
			string body = string.Format("Hi,<br /><br />Please Click on the given <a href='" + url + "'>link</a> to activate your account.<br /><br />Thank You.");
			return body;
		}
		public static string ResetEmailBody(string Guid)
		{
			var url = httpContextAccessor.HttpContext.Request.Scheme + "://" + httpContextAccessor.HttpContext.Request.Host + "/Home/ActivateAccount?token=" + Guid;
			string body = string.Format("Hi,<br /><br />Please Click on the given <a href='" + url + "'>link</a> to Rest your password.<br /><br />Thank You.");
			return body;
		}
		//public static string SendEmail(ref string log, string subject, string body, string To, TblEmailCredentials emailCredentials)
		//{
		//	try
		//	{
		//		if (emailCredentials != null)
		//		{
		//			var From = emailCredentials.UserName;
		//			MailMessage mail = new MailMessage((From), (To), subject, body);
		//			SmtpClient SmtpServer = new SmtpClient(emailCredentials.Smtp);
		//			mail.IsBodyHtml = Convert.ToBoolean(emailCredentials.IsBodyHtml);
		//			SmtpServer.Port = Convert.ToInt32(emailCredentials.Port);
		//			SmtpServer.Credentials = new System.Net.NetworkCredential(emailCredentials.UserName, emailCredentials.Password);
		//			SmtpServer.EnableSsl = Convert.ToBoolean(emailCredentials.EnableSsl);
		//			SmtpServer.Send(mail);
		//			return "Check Your Email";
		//		}
		//		else
		//		{
		//			return SomethingWntWrong;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		log = ex.Message;
		//		return ex.Message.ToString();
		//	}

		//}

		public static string ExceptionLogBulder(string log, string methodName, Exception ex)
		{
			if (!string.IsNullOrEmpty(log))
			{
				log = log + Environment.NewLine;
			}
			log += "An exception has been occurred in Method: " + methodName + DateTime.Now.ToString() + " ExceptionMessage: " + ex.Message;
			return log;
		}

		public static string ActivityAddlogDescription(string action, string performedOn, int performedOnId)
		{
			int loggedInUserId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
			string loggedInUser = httpContextAccessor.HttpContext.User.Identity.Name;
			string hostName = Dns.GetHostName(); // Retrive the Name of HOST
			string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
			var ActivityDescription = "";
			if (action == "Logged_Out")
			{
				ActivityDescription = "User " + loggedInUser + " bearing Id: " + loggedInUserId + " has " + action + " at " + DateTime.Now + " from " + Environment.MachineName + " having IP: " + myIP;
			}
			else
			{
				ActivityDescription = "User " + loggedInUser + " bearing Id: " + loggedInUserId + " has " + action + " " + performedOn + " bearing Id: " + performedOnId + " at " + DateTime.Now + " from " + Environment.MachineName + " having IP: " + myIP;
			}
			return ActivityDescription;
		}

		public static string ActivityloginDescription(string loggedInUser, int loggedInUserId, string action)
		{
			string hostName = Dns.GetHostName(); // Retrive the Name of HOST
			string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
			var ActivityDescription = "User " + loggedInUser + " bearing Id: " + loggedInUserId + " has " + action + " at " + DateTime.Now + " from " + Environment.MachineName + " having IP: " + myIP;
			return ActivityDescription;
		}

		public static string ActivityActivationLog(string action, int id)
		{
			string hostName = Dns.GetHostName(); // Retrive the Name of HOST
			string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
			var ActivityDescription = "User bearing Id: " + id + " has " + action + " acount at " + DateTime.Now + " from " + Environment.MachineName + " having IP: " + myIP;
			return ActivityDescription;
		}
		public static string SaveImgToDirectory(IFormFile file, string uploadPath)
		{
			if (!Directory.Exists(siteOptions.DirectoryPath + "/" + uploadPath))
			{
				Directory.CreateDirectory(siteOptions.DirectoryPath + "/" + uploadPath);
			}
			var fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("MMddyyyyHHmmss");
			fileName += Path.GetExtension(file.FileName);
			var uploadPathWithfileName = Path.Combine(uploadPath, fileName);
			var uploadAbsolutePath = Path.Combine(siteOptions.DirectoryPath, uploadPathWithfileName);
			using (var fileStream = new FileStream(uploadAbsolutePath, FileMode.Create))
			{
				file.CopyTo(fileStream);
			}
			return uploadPathWithfileName;
		}

		public static String ConvertImageToBase64String(IFormFile file)
		{
			var ms = new MemoryStream();
			file.OpenReadStream().CopyTo(ms);
			byte[] imageBytes = ms.ToArray();

			// var imagePath = System.IO.Path.Get(file.FileName);
			//  byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
			string base64String = Convert.ToBase64String(imageBytes);
			return base64String;
		}

		public static string SaveFileToDirectory(IFormFile file, string uploadPath)
		{
			if (!Directory.Exists(siteOptions.DirectoryPath + "/" + uploadPath))
			{
				Directory.CreateDirectory(siteOptions.DirectoryPath + "/" + uploadPath);
			}
			var fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("MMddyyyyHHmmss");
			fileName += Path.GetExtension(file.FileName);
			string ext = Path.GetExtension(file.FileName);
			if (ext.ToLower() != ".pdf")
			{
				return "Wrong File";
			}
			var uploadPathWithfileName = Path.Combine(uploadPath, fileName);
			var uploadAbsolutePath = Path.Combine(siteOptions.DirectoryPath, uploadPathWithfileName);

			using (var fileSteam = new FileStream(uploadAbsolutePath, FileMode.Create))
			{
				file.CopyTo(fileSteam);
			}
			return uploadPathWithfileName;
		}
		public static string GetFileNameSavedIndb(IFormFile file, string uploadPath)
		{
			var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
			var uploadPathWithfileName = Path.Combine(uploadPath, fileName);
			return uploadPathWithfileName;
		}

		public static bool checkBln(bool? value)
		{
			if (value == null)
			{
				return false;
			}
			return true;
		}
		public enum ActivityActions
		{
			Added = 0,
			Updated = 1,
			Deleted = 2,
			Logged_In = 3,
			Logged_out = 4,
			Deactivated = 4,
			Activated = 5,
			Changed_Password = 6,
		}
	}

	public static class siteOptions
	{
		public static string DirectoryPath { get; set; }
		public static string URL { get; set; }
	}


}
