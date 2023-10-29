using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib
{
    public class ConnectionDB
    {
        private static string connectionString { get; set; }
        private static string providerName { get; set; }

        static ConnectionDB()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                providerName = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao obter connection string.");
            }
        }

        public static string ConnectionString { get { return connectionString; } }
        public static string ProviderName { get { return providerName; } }
    }
}
