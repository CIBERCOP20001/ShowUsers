using ShowUsers.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowUsers.Model.ViewModels
{
    public class AppUserDataTableViewModel
    {
        public List<AppUser> AppUserList { get; set; }
        public int AppUsersTotal { get; set; }

    }
}
