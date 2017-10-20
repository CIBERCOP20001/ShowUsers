using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowUsers.Model.Models
{
    public class AppUser
    {
        public string Active { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
