using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowUsers.Model;
using ShowUsers.Model.Custom;
using ShowUsers.Model.Models;
using ShowUsers.Model.ViewModels;

namespace ShowUsers.Interface
{
    public interface IAccountRepository
    {
        bool ValidateLogin(ShowUsers.Model.Models.Login login);
        AppUserDataTableViewModel GetAppUsersList(JQueryDatatableParamModel param);
        string UpdateUserStatus(string username, bool v);
        bool CreateUser(AppUser appuser);
    }
}
