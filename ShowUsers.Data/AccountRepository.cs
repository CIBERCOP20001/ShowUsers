using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowUsers.Interface;
using ShowUsers.Model.Models;

namespace ShowUsers.Data
{
    public class AccountRepository : IAccountRepository
    {
        public bool ValidateLogin(Login login)
        {
            throw new NotImplementedException();
        }
    }
}
