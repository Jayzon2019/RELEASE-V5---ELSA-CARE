using InLifeCMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using InLifeCMS.Models;
using System.Threading.Tasks;
using InLifeCMS.Helpers;
using InLifeCMS.Services;

namespace InLifeCMS.Repos
{

    public class UsersRepo
    {
        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
       // dbinlifecmshostContext db = new dbinlifecmshostContext();
        LogsRepo lR = new LogsRepo();
        LogsService LS = new LogsService();
        public List<UsersViewModel> GetUsers(ref string log)
        {
            try
            {
                var users = (from u in db.TblUsers
                             join r in db.TblUserRoles on u.UserRoleId equals r.UserRoleId
                             join g in db.TblGender on u.GenderId equals g.GenderId
                             where u.IsActive == true 
                             orderby u.CreatedDate descending
                             select new UsersViewModel
                             {
                                 intUserId = u.UserId,
                                 strFirstName = u.FirstName,
                                 strLastName = u.LastName,
                                 strEmail = u.Email,
                                 dteDob = u.Dob,
                                 strUserImg = u.UserImg,
                                 strPhone = u.Phone,
                                 strGender = g.Gender,
                                 strUserRole = r.UserRole
                             }).ToList();

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

        public List<TblGender> GetGenders(ref string log)
        {
            try
            {
                var genders = db.TblGender.ToList();

                return genders;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public List<TblUserRoles> GetRoles(ref string log)
        {
            try
            {
                var Roles = db.TblUserRoles.ToList();

                return Roles;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void SaveUser(ref string log, TblUsers user)
        {
            try
            {
                var Addeduser = db.TblUsers.Add(user);
                db.SaveChanges();
                if (Addeduser.Entity.UserId > 0)
                {
                    if (log != " ")
                    {
                        string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        Exception ex = new Exception();
                        var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                        lR.SaveExceptionLogs(exLog, ex, methodName);

                    }
                    var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Added.ToString(), "a new user", Addeduser.Entity.UserId);
                    LS.SaveActivityLogs(Comman.ActivityActions.Added.ToString(), activityLog);
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public TblUsers GetUserById(ref string log, int? id)
        {
            try
            {
                var user = db.TblUsers.Where(x => x.UserId == id).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditUser(ref string log, TblUsers user)
        {
            try
            {
                var oldUser = db.TblUsers.Where(x => x.UserId == user.UserId && x.IsArchived == false && x.ActivationDate != null).FirstOrDefault();
                oldUser.Dob = user.Dob;
                oldUser.Email = user.Email;
                oldUser.FirstName = user.FirstName;
                oldUser.GenderId = user.GenderId;
                oldUser.LastName = user.LastName;
                oldUser.Phone = user.Phone;
                oldUser.Password = user.Password;
                oldUser.UserId = user.UserId;
                if (user.UserImg != null && user.UserImg != "")
                {
                    oldUser.UserImg = user.UserImg;

                }
                if (user.UserRoleId > 0)
                {
                    oldUser.UserRoleId = user.UserRoleId;
                }
                oldUser.UpdatedDate = user.UpdatedDate;
                oldUser.UpdatedBy = user.UpdatedBy;
                db.TblUsers.Update(oldUser);
                db.SaveChanges();
                var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Updated.ToString(), "user", oldUser.UserId);
                LS.SaveActivityLogs(Comman.ActivityActions.Updated.ToString(), activityLog);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public bool CheckEmail(ref string log, string email, int id)
        {
            try
            {
                bool isExist = false;
                var users = db.TblUsers.Where(x => x.Email == email && x.IsActive == true && x.ActivationDate != null).ToList();
                foreach (var u in users)
                {
                    if (id > 0)
                    {
                        if (u.UserId != id && u.Email == email)
                        {
                            isExist = true;
                            break;
                        }
                    }
                    else
                    {
                        if (u.Email == email)
                        {
                            isExist = true;
                        }
                    }
                }
                return isExist;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return true;
            }
        }

        public bool CheckImg(ref string log, string newImg)
        {
            try
            {
                bool ImgExists = db.TblUsers.Any(x => x.UserImg == newImg && x.IsActive == true && x.ActivationDate != null);
                if (ImgExists)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return true;
            }
        }

        public void Deactivate_DeleteUser(ref string log, int id, bool delete)
        {
            try
            {
                if (delete)
                {
                    var user = db.TblUsers.Where(x => x.IsActive == true && x.UserId == id).FirstOrDefault();
                    if (user != null)
                    {
                        user.IsArchived = true;
                        user.IsActive = false;
                        db.TblUsers.Update(user);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deleted.ToString(), "user", user.UserId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deleted.ToString(), activityLog);
                    }
                }
                else
                {
                    var user = db.TblUsers.Where(x => x.IsActive == true && x.UserId == id).FirstOrDefault();
                    if (user != null)
                    {
                        user.IsArchived = false;
                        user.IsActive = true;
                        db.TblUsers.Update(user);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deactivated.ToString(), "user", user.UserId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deactivated.ToString(), activityLog);
                    }
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public string GETUserCreatedBy_UpdatedBy(ref string log, int id)
        {
            try
            {
                var usr = db.TblUsers.Where(x => x.UserId == id).FirstOrDefault();
                if (usr != null)
                {
                    return usr.FirstName + " " + usr.LastName;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return "";
            }
        }

        public string GetRoleById(ref string log, int roleId)
        {
            try
            {
                var role = db.TblUserRoles.Where(x => x.UserRoleId == roleId).FirstOrDefault();
                return role.UserRole;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return "";
            }

        }

        public string GetGenderById(ref string log, int Id)
        {
            try
            {
                var gen = db.TblGender.Where(x => x.GenderId == Id).FirstOrDefault();
                return gen.Gender;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return "";
            }

        }

        public bool ChangePassword(ref string log, string pass, int id)
        {
            try
            {
                bool isChanged = false;
                var user = db.TblUsers.Where(x => x.IsActive == true && x.UserId == id).FirstOrDefault();
                user.Password = pass;
                db.TblUsers.Update(user);
                db.SaveChanges();
                isChanged = true;
                var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Changed_Password.ToString(), "of user", user.UserId);
                LS.SaveActivityLogs(Comman.ActivityActions.Changed_Password.ToString(), activityLog);
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

        public bool ActivateUser(ref string log, TblUsers user)
        {
            bool isActivated = false;
            try
            {
                db.TblUsers.Update(user);
                db.SaveChanges();
                isActivated = true;
                var activityLog = Comman.ActivityActivationLog(Comman.ActivityActions.Activated.ToString(), user.UserId);
                LS.SaveActivityLogs(Comman.ActivityActions.Activated.ToString(), activityLog);
                return isActivated;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return false;
            }
        }

    }
}
