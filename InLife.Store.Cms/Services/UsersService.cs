using InLifeCMS.Helpers;
using InLifeCMS.Repos;
using InLifeCMS.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using InLife.Store.Core.Models;

namespace InLifeCMS.Services
{
	public class UsersService
	{
		UsersRepo UR = new UsersRepo();
		LogsRepo lR = new LogsRepo();

		private static IHttpContextAccessor httpContextAccessor;
		public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
		{
			httpContextAccessor = accessor;
		}
		public List<UsersViewModel> GetUsers(ref string log)
		{
			try
			{
				var users = UR.GetUsers(ref log);
				return users;
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return null;
			}

		}

		public List<GenderViewModel> GetGenders(ref string log)
		{
			try
			{
				var Genders = UR.GetGenders(ref log);
				List<GenderViewModel> genList = new List<GenderViewModel>();
				foreach (var gen in Genders)
				{
					GenderViewModel GVM = new GenderViewModel();
					GVM.intGenderId = gen.GenderId;
					GVM.strGender = gen.Gender;
					genList.Add(GVM);
				}
				return genList;
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return null;
			}

		}

		public List<UserRolesViewModel> GetRoles(ref string log)
		{
			try
			{
				var Roles = UR.GetRoles(ref log);
				List<UserRolesViewModel> roleList = new List<UserRolesViewModel>();
				foreach (var role in Roles)
				{
					UserRolesViewModel URVM = new UserRolesViewModel();
					URVM.intUserRoleId = role.UserRoleId;
					URVM.strUserRole = role.UserRole;
					roleList.Add(URVM);
				}
				return roleList;
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return null;
			}

		}

		public string SaveUser(ref string log, UsersViewModel usersViewModel)
		{
			try
			{
				var uploadPathWithfileName = "";
				var files = httpContextAccessor.HttpContext.Request.Form.Files;
				if (files != null && files.Count() > 0)
				{
					var file = files[0];
					if (file != null && file.Length > 0)
					{
						string uploadPath = "images/UserImgs";
						uploadPathWithfileName = Comman.SaveImgToDirectory(file, uploadPath);
					}
				}
				var pass = Comman.Encrypt(usersViewModel.strPassword);
				TblUsers user = new TblUsers
				{
					CreatedDate = DateTime.Now,
					ActivationDate = DateTime.Now,
					CreatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
					FirstName = usersViewModel.strFirstName,
					LastName = usersViewModel.strLastName,
					UserImg = uploadPathWithfileName,
					UserRoleId = usersViewModel.intUserRoleId,
					GenderId = usersViewModel.intGenderId,
					Phone = usersViewModel.strPhone,
					Dob = usersViewModel.dteDob,
					Email = usersViewModel.strEmail,
					Password = pass,
					IsActive = true,
				};
				bool isEmailExist = UR.CheckEmail(ref log, user.Email, user.UserId);
				if (isEmailExist)
				{
					return "Email already exists please try other email!";
				}
				UR.SaveUser(ref log, user);
				return "Saved";
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return Comman.SomethingWntWrong;
			}
		}

		public UsersViewModel GetUserById(ref string log, int? id)
		{
			try
			{
				var user = UR.GetUserById(ref log, id);
				var pass = Comman.Decrypt(user.Password);
				var img = "";
				if (user.UserImg != null && user.UserImg != "")
				{
					img = user.UserImg;
				}
				else
				{
					img = "images/UserImgs/avatar.jpg";
				}
				UsersViewModel UVM = new UsersViewModel
				{
					intCreatedBy = Convert.ToInt32(user.CreatedBy),
					intGenderId = Convert.ToInt32(user.GenderId),
					intUserId = user.UserId,
					intUserRoleId = user.UserRoleId,
					strUserImg = img,
					strFirstName = user.FirstName,
					strLastName = user.LastName,
					strEmail = user.Email,
					strPhone = user.Phone,
					strPassword = pass,
					intUpdatedBy = user.UpdatedBy,
					dteActivationDate = user.ActivationDate,
					blnIsActive = user.IsActive,
				};
				if (user.CreatedDate != null)
				{
					UVM.dteCreatedDate = Comman.getClientTime(user.CreatedDate.ToString());
				}
				if (user.UpdatedDate != null)
				{
					UVM.dteUpdatedDate = Comman.getClientTime(user.UpdatedDate.ToString());
				}
				var createdBy = UR.GETUserCreatedBy_UpdatedBy(ref log, UVM.intCreatedBy);
				var role = UR.GetRoleById(ref log, UVM.intUserRoleId);
				UVM.strUserRole = role;
				var gender = UR.GetGenderById(ref log, UVM.intUserRoleId);
				UVM.strGender = gender;
				UVM.strCreatedByUser = createdBy;
				if (UVM.intUpdatedBy > 0)
				{
					if (UVM.intCreatedBy != UVM.intUpdatedBy)
					{
						int uId = Convert.ToInt32(UVM.intUpdatedBy);
						UVM.strUpdatedByUser = UR.GETUserCreatedBy_UpdatedBy(ref log, uId);
					}
					else
					{
						UVM.strUpdatedByUser = createdBy;
					}
				}

				return UVM;
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return null;
			}
		}

		public void EditUser(ref string log, UsersViewModel user)
		{
			try
			{
				string uploadPath = "images/UserImgs";
				bool isDifferentImg = true;
				var pass = Comman.Encrypt(user.strPassword);
				TblUsers u = new TblUsers
				{
					Dob = user.dteDob,
					Email = user.strEmail,
					FirstName = user.strFirstName,
					LastName = user.strLastName,
					Phone = user.strPhone,
					GenderId = user.intGenderId,
					UserId = user.intUserId,
					UserRoleId = user.intUserRoleId,
					Password = pass,
					UserImg = user.strUserImg,
					UpdatedDate = DateTime.Now,
					UpdatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value)
				};
				var files = httpContextAccessor.HttpContext.Request.Form.Files;
				if (files.Count > 0)
				{
					var file = files[0];
					if (file != null && file.Length > 0)
					{
						var newFile = Comman.GetFileNameSavedIndb(file, uploadPath);
						isDifferentImg = UR.CheckImg(ref log, newFile);
						if (isDifferentImg)
						{
							var uploadPathWithfileName = Comman.SaveImgToDirectory(file, uploadPath);
							u.UserImg = uploadPathWithfileName;
						}
					}
				}
				bool isEmailExist = UR.CheckEmail(ref log, u.Email, u.UserId);
				if (isEmailExist)
				{
					return;
				}
				UR.EditUser(ref log, u);

			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
			}
		}

		public void Deactivate_DeleteUser(ref string log, int id, bool delete)
		{
			try
			{
				UR.Deactivate_DeleteUser(ref log, id, delete);
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
			}
		}

		public bool ChangePassword(ref string log, string Newpass)
		{
			try
			{
				var encryptedPass = Comman.Encrypt(Newpass);
				var id = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
				bool isChanged = UR.ChangePassword(ref log, encryptedPass, id);
				return isChanged;
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return false;
			}

		}

		public bool SetPassword(ref string log, int id, string Newpass)
		{
			try
			{
				var encryptedPass = Comman.Encrypt(Newpass);
				bool isChanged = UR.ChangePassword(ref log, encryptedPass, id);
				return isChanged;
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return false;
			}

		}


		public UsersViewModel VerifyToken(ref string log, int id, string token)
		{
			try
			{
				var user = UR.GetUserById(ref log, id);
				if (user.ActivationCode == token)
				{
					user.IsActive = true;
					user.ActivationDate = DateTime.Now;
					bool isActivated = UR.ActivateUser(ref log, user);
					if (isActivated)
					{
						UsersViewModel uvm = new UsersViewModel();
						uvm.intUserId = user.UserId;
						return uvm;
					}
					else
					{
						return null;
					}
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return null;
			}
		}
	}

}
