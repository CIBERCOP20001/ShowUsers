using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowUsers.Utility
{
    public static class Common
    {
        public static string GetConnectionString()
        {
            var connString = ConfigurationManager.ConnectionStrings[Constants.dbConnectionName].ConnectionString;

            return connString;
        }
    }
}
