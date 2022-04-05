using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using MainLibrary;

namespace Ticketing.Client
{
    public class CLI
    {
        string ConnectionString { get; }

        private Dictionary<string, string> _commandInfo;

        private Dictionary<string, Action> _commandAction;

        public CLI(string connectionString)
        {
            ConnectionString = connectionString;

            _commandInfo = new Dictionary<string, string>
            {
                { "LT", "List all tickets (in chronological order)" },
                { "IT", "Insert new ticket" },
                { "DT", "Delete ticket" },
                { "H", "Show this menu" },
                { "Q", "Exit the program" }
            };

            _commandAction = new Dictionary<string, Action>
            {
                { "LT", () => Handle(ListTickets) },
                { "IT", () => Handle(InsertTicket) },
                { "DT", () => Handle(DeleteTicket) },
                { "H", ShowMenu },
                { "Q", ExitProgram }
            };
        }

        public void Handle(Action<SqlConnection> handled)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                handled(connection);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GENERIC ERROR:  {ex.Message}");
            }
            finally
            {   
                if (connection.State != System.Data.ConnectionState.Closed)
                    connection.Close();
            }
        }

        public void ListTickets(SqlConnection connection)
        {
            string sqlText = "SELECT * FROM ticket ORDER BY opened DESC";

            SqlCommand cmd = new SqlCommand(sqlText, connection);

            var reader = cmd.ExecuteReader();

            Console.WriteLine(new String('-', Console.BufferWidth));
            Console.WriteLine("{0,5} {1,-60} {2,10} {3,20} {4,10}", "ID", "Description", "Opened", "Customer", "State");
            Console.WriteLine(new String('-', Console.BufferWidth));

            // Print result of the query
            while (reader.Read())
            {
                var id = reader["id"];
                var description = reader["description"];
                var opened = $"{reader["opened"]:d}";
                var customer = reader["customer"];
                var state = reader["state"];

                Console.WriteLine("{0,5} {1,-60} {2,10} {3,20} {4,10}", id, description, opened, customer, state);
            }

            Console.WriteLine(new String('-', Console.BufferWidth));
        }

        public void InsertTicket(SqlConnection connection)
        {
            string sqlText = "InsertTicket";

            SqlCommand cmd = new SqlCommand(sqlText, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            string description = InputLib.ReadFromConsoleConditionally(
                        "\nInsert a description for the new ticket: ",
                        x => !String.IsNullOrEmpty(x) && !String.IsNullOrWhiteSpace(x));

            string customer = InputLib.ReadFromConsoleConditionally(
                        "\nInsert the name of the customer for the new ticket: ",
                        x => !String.IsNullOrEmpty(x) && !String.IsNullOrWhiteSpace(x));

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@description",
                Value = description,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 500
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@customer",
                Value = customer,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 100
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@msg",
                Value = "",
                SqlDbType= System.Data.SqlDbType.VarChar
            });

            SqlParameter returnValue = new SqlParameter()
            {
                ParameterName = "@ReturnValue",
                Direction = System.Data.ParameterDirection.ReturnValue
            };
            cmd.Parameters.Add(returnValue);

            // Execute stored procedure
            int result = cmd.ExecuteNonQuery();

            if (result == 1)
                Console.WriteLine("\nSUCCESS: Stored procedure executed.");
            else
                Console.WriteLine("\nFAILED: There were problems executing stored procedure.");

            if (int.Parse(returnValue.Value.ToString()) != 0)
                Console.WriteLine("\nNOTE: The stored procedure returned something different from 0.");
        }

        public void DeleteTicket(SqlConnection connection)
        {
            int id = 0;

            InputLib.ReadFromConsoleConditionally(
                "Please insert ID of the ticket to be deleted: ",
                s => int.TryParse(s, out id));

            string sqlText = $"DELETE FROM ticket WHERE id = @id";

            SqlCommand cmd = new SqlCommand(sqlText, connection);

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@id",
                Value = id,
                SqlDbType = System.Data.SqlDbType.Int
            });
            
            int result = cmd.ExecuteNonQuery();

            if (result == 1)
                Console.WriteLine($"SUCCESS: Deleted ticket {id}.");
            else
                Console.WriteLine($"FAILED: There were problems deleting ticket {id}.");

            Console.WriteLine();
        }

        public void ShowMenu()
        {
            Console.WriteLine("\nPlease choose one of the following options:");
            Console.WriteLine("===========================================");
            foreach (var command in _commandInfo)
                Console.WriteLine($"    |\t{command.Key} \t | {command.Value}");
            Console.WriteLine();
        }

        public void ExitProgram()
        {
            Environment.Exit(0);
        }

        public void Run()
        {
            Console.WriteLine("=== WELCOME TO YOUR FRIENDLY TICKETING SERVICE ===");

            ShowMenu();

            do
            {
                string input = InputLib.ReadFromConsoleConditionally(
                    "Please choose a valid option: ",
                    s =>  _commandInfo.Keys.Contains(s.ToUpper()));

                // Match the _commandInfo key
                input = input.ToUpper();

                // Launch the command
                _commandAction[input]();

                Console.WriteLine();
            } while (true);
        }
    }
}