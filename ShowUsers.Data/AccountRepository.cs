using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowUsers.Interface;
using ShowUsers.Model.Models;
using Dapper;
using ShowUsers.Utility;
using System.Data.SqlClient;
using System.Data;
using ShowUsers.Model.Custom;
using ShowUsers.Model.ViewModels;

namespace ShowUsers.Data
{
    public class AccountRepository : IAccountRepository
    {
        string connString = Utility.Common.GetConnectionString();

        public bool CreateUser(AppUser appuser)
        {
            bool status = true;
            int result = 0;
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    result = conn.Query<int>("spInsertNewUser",
                               new
                               {
                                   Email = appuser.Email,
                                   Gender = appuser.Gender,
                                   Password = appuser.Password,
                                   UserName = appuser.UserName,
                                   Active = appuser.Active
                               },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();
                    status = (result > 0) ? true : false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public AppUserDataTableViewModel GetAppUsersList(JQueryDatatableParamModel param)
        {
            AppUserDataTableViewModel data = new AppUserDataTableViewModel();
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var multi =
                       conn.QueryMultiple
                       ("spGetAppUsersList",
                                   new
                                   {
                                       DisplayLength = param.iDisplayLength,
                                       DisplayStart = param.iDisplayStart,
                                       SortOrder = param.sSortDir_0,
                                       SortCol = param.iSortCol_0
                                   }, commandTimeout: 0, commandType: CommandType.StoredProcedure
                       )
                  )
                    {
                        data.AppUserList = multi.Read<AppUser>().ToList();
                        data.AppUsersTotal = multi.Read<Int32>().SingleOrDefault();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return data;
        }

        public string UpdateUserStatus(string email, bool newStatus)
        {
            string status = string.Empty;
            int result = 0;
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    result = conn.Query<int>("spUpdateUserStatus",
                               new
                               {
                                   Email = email,
                                   Status = Convert.ToByte(newStatus)
                               },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();
                    status = (result > 0) ? newStatus.ToString() : "Fail";
                }
            }
            catch (Exception ex)
            {
                status = "Fail";
            }
            return status;
        }

        public bool ValidateLogin(Login login)
        {
            bool isValid = true;
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    isValid = conn.Query<bool>("spValidateLogin",
                               new
                               {
                                   LoginUser = login.LoginUser,
                                   LoginPassword = login.Password
                               },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();
                }

            }
            catch (Exception)
            {
                isValid = false;
            }
            return isValid;
        }
    }
}
