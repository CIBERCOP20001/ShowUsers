using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowUsers.Model;

namespace ShowUsers.Interface
{
    public interface IAccountRepository
    {
        bool ValidateLogin(ShowUsers.Model.Models.Login login);
    }
}
