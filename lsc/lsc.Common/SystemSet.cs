using Microsoft.Extensions.Configuration;
using System;

namespace lsc.Common
{
    public class SystemSet
    {
        private static IConfiguration configuration;

        private static string _MysqlConnectionString= "";

        public static string MysqlConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_MysqlConnectionString) && configuration!=null)
                    _MysqlConnectionString = configuration.GetConnectionString("MySqlConnection");
                if (_MysqlConnectionString.IsNull())
                {
                    _MysqlConnectionString = "Server=localhost;database=crm;uid=root;pwd=xqpsy;";
                }
                return _MysqlConnectionString;
            }
        }

        public static void Ini(IConfiguration cfg)
        {
            configuration = cfg;
        }
    }
}
