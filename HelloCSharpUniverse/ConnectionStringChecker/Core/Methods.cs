using EasyConsoleFramework.IO;
using EasyConsoleFramework.Utils;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;

namespace ConnectionStringChecker.Core
{
    /// <summary>
    /// Check a connection string read from the command line interface input
    /// </summary>
    public static class Methods
    {
        public static void CheckConnectionFromCLI()
        {
            string connectionString = BaseIO.ReadFromConsole(
                "\tPlease type the connection string followed by [Enter]: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));
            
            CheckConnection(connectionString);
        }

        /// <summary>
        /// Check one or more connection strings read from an ASCII file
        /// </summary>
        public static void CheckConnectionsFromASCII()
        {
            string path = BaseIO.ReadFromConsole(
                "\tPlease insert the file path followed by [Enter]: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

            if (!File.Exists(path))
                throw new FileNotFoundException();

            string line;
            
            using (StreamReader reader = new StreamReader(path))
            {
                while ((line = reader.ReadLine()) != null)
                    CheckConnection(line);
            }
        }

        private static void CheckConnection(string connectionString)
        {
            SqlConnection connection = null;
            
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                
                Console.WriteLine("SUCCESS!");
            }
            catch (Exception ex)
            {
                ErrorHandling.Catch(ex);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }
    }
}