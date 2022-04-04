using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using MainLibrary;

namespace PizzaRestaurant.Client
{
    public static class AdoNetExamples
    {
        private static IConfigurationRoot config =
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        private static string cs = config.GetConnectionString("pizza_restaurant");

        public static void ConnectionExample()
        {
            // Connection string
            //string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=pizza_restaurant;Integrated Security=True";

            // Create connection
            using (var connection = new SqlConnection(cs))
            {
                // Open connection
                connection.Open();

                Console.WriteLine("Opening connection synchronously...");

                // Check connection state
                if (connection.State == System.Data.ConnectionState.Open)
                     Console.WriteLine("SUCCESS: Connection to data source established.");
                else
                    Console.WriteLine("FAILED: Not able to connect to data source.");
                    
                // Close connection
                connection.Close();
            }
        }

        public static async Task ConnectionExampleAsync()
        {
            // Connection string
            //string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=pizza_restaurant;Integrated Security=True";

            // Create connection
            using (var connection = new SqlConnection(cs))
            {
                // Open connection
                //var taskOpening = connection.OpenAsync();
                //await taskOpening;
                await connection.OpenAsync();

                Console.WriteLine("Opening connection asynchronously...");

                // Check connection state
                if (connection.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("SUCCESS: Connection to data source established.");
                else
                    Console.WriteLine("FAILED: Not able to connect to data source.");

                // Close connection
                await connection.CloseAsync();
            }
        }

        public static void DataReaderExample()
        {
            // Connection string
            //string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=pizza_restaurant;Integrated Security=True";
            
            using (var connection = new SqlConnection(cs))
            {
                try
                {
                    // Define SQL query to be executed
                    string sqlCommand = "SELECT * FROM pizza";

                    // Open connection
                    connection.Open();

                    // METHOD 1
                    //var readCommand = new SqlCommand(sqlCommand, connection)

                    // METHOD 2
                    //var readCommand = new SqlCommand();
                    //readCommand.Connection = connection as SqlConnection;
                    //readCommand.CommandTest = sqlCommand;
                    //readCommand.CommandType = System.Data.CommandType.Text;

                    // METHOD 3
                    //var readCommand = connection.CreateCommand();
                    //readCommand.CommandText = sqlCommand;
                    //readCommand.CommandType = System.Data.CommandType.Text;

                    using (DbCommand readCommand = new SqlCommand(sqlCommand, connection))
                    {
                        var reader = readCommand.ExecuteReader();

                        Console.WriteLine("Pizzas!");
                        Console.WriteLine(new String('-', 50));

                        Console.WriteLine("{0,5} {1,-25} {2,10}", "ID", "Name", "Price ($)");

                        // Print result of the query
                        while (reader.Read())
                        {
                            object id = reader["id"];           // Associative, dynamic-typed access
                            object name = reader[1];            // Indexed, dynamic-typed access
                            var price = reader.GetDecimal(2);   // Indexed, strong-typed acces

                            Console.WriteLine("{0,5} {1,-25} {2,10}", id, name, price);
                        }

                        Console.WriteLine(new String('-', 50));
                    }

                    // Close connection
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"GENERIC ERROR: {ex.Message}");
                }
                finally
                {
                    // Close connection if still open
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static void DataReaderWithParamsExample(decimal threshold)
        {
            // Connection string
            //string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=pizza_restaurant;Integrated Security=True";

            using (var connection = new SqlConnection(cs))
            {
                try
                {
                    // Create (typed) parameter
                    SqlParameter priceParam = new SqlParameter();
                    priceParam.ParameterName = "@price";
                    priceParam.Value = threshold;
                    priceParam.SqlDbType = System.Data.SqlDbType.Money;

                    // Define SQL query to be executed
                    string sqlCommand = "SELECT id, name, price FROM pizza WHERE price >= @price";

                    // Open connection
                    connection.Open();

                    using (DbCommand readCommand = new SqlCommand(sqlCommand, connection))
                    {
                        // Add parameter to command
                        readCommand.Parameters.Add(priceParam);

                        var reader = readCommand.ExecuteReader();

                        Console.WriteLine("Pizzas!");
                        Console.WriteLine(new String('-', 50));

                        Console.WriteLine("{0,5} {1,-25} {2,10}", "ID", "Name", "Price ($)");

                        // Print result of the query
                        while (reader.Read())
                        {
                            object id = reader["id"];           // Associative, dynamic-typed access
                            object name = reader[1];            // Indexed, dynamic-typed access
                            var price = reader.GetDecimal(2);   // Indexed, strong-typed acces

                            Console.WriteLine("{0,5} {1,-25} {2,10}", id, name, price);
                        }

                        Console.WriteLine(new String('-', 50));
                    }

                    // Close connection
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"GENERIC ERROR: {ex.Message}");
                }
                finally
                {
                    // Close connection if still open
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static void InsertExample()
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    connection.Open();

                    string sqlText = "INSERT INTO ingredient VALUES ('Caviar', 200.0, 10)";
                    SqlCommand cmd = new SqlCommand(sqlText, connection);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                        Console.WriteLine("SUCCESS: Ingredient correctly inserted.");
                    else
                        Console.WriteLine("FAILED: Not able to insert ingredient.");

                    Console.WriteLine();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static void InsertWithParamsExample()
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    connection.Open();

                    Console.Write("Insert ingredient name: ");
                    string ingredient = Console.ReadLine();

                    string sqlText = "INSERT INTO ingredient VALUES (@name, 1, 1)";
                    SqlCommand cmd = new SqlCommand(sqlText, connection);

                    SqlParameter nameParam = cmd.CreateParameter();
                    nameParam.ParameterName = "@name";
                    nameParam.Value = ingredient;
                    nameParam.SqlDbType = System.Data.SqlDbType.VarChar;
                    nameParam.Size = 50;
                    
                    cmd.Parameters.Add(nameParam);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                        Console.WriteLine("SUCCESS: Ingredient correctly inserted.");
                    else
                        Console.WriteLine("FAILED: Not able to insert ingredient.");

                    Console.WriteLine();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static void ExecuteScalarExample()
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    connection.Open();

                    string sqlText = "SELECT COUNT(*) FROM ingredient";
                    SqlCommand cmd = new SqlCommand(sqlText, connection);

                    var result = cmd.ExecuteScalar();
                    int count = int.Parse(result.ToString());

                    Console.WriteLine($"Number of ingredients in database: {count}");

                    Console.WriteLine();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static void CallStoredProcedureWithParamsExample()
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = "AddNewPizza";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    string name = InputLib.ReadFromConsoleConditionally(
                        "Insert a name for the new pizza: ",
                        x => !String.IsNullOrEmpty(x) && !String.IsNullOrWhiteSpace(x));

                    decimal price = 99999.99m;

                    InputLib.ReadFromConsoleConditionally(
                        "Insert pizza price: ",
                        x => decimal.TryParse(x, out price));

                    SqlParameter nameParam = new SqlParameter {
                        ParameterName = "@name",
                        Value = name,
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 25
                    };

                    SqlParameter priceParam = new SqlParameter {
                        ParameterName = "@price",
                        Value = price,
                        SqlDbType = System.Data.SqlDbType.Money
                    };

                    SqlParameter msgParam = new SqlParameter("@msg", System.Data.SqlDbType.Text) {
                        ParameterName = "@msg",
                        Value = "",
                        SqlDbType = System.Data.SqlDbType.VarChar
                    };

                    SqlParameter returnParam = new SqlParameter() {
                        ParameterName = "@ReturnVal",
                        Direction = System.Data.ParameterDirection.ReturnValue
                    };

                    cmd.Parameters.Add(nameParam);
                    cmd.Parameters.Add(priceParam);
                    cmd.Parameters.Add(msgParam);
                    cmd.Parameters.Add(returnParam);

                    // Execute stored procedure
                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                        Console.WriteLine("SUCCESS: Stored procedure executed.");
                    else
                        Console.WriteLine("FAILED: There were problems executing stored procedure.");

                    if (int.Parse(returnParam.Value.ToString()) != 0)
                        Console.WriteLine("NOTE: The stored procedure returned something different from 0.");

                    Console.WriteLine();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static void UpdateExample()
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    connection.Open();

                    string sqlText = "UPDATE pizza SET price = price * 1.1 WHERE name = 'Margherita'";

                    SqlCommand cmd = new SqlCommand(sqlText, connection);

                    var result = cmd.ExecuteNonQuery();

                    if (result == 1)
                        Console.WriteLine("SUCCESS: Price of Margherita pizza increased.");
                    else
                        Console.WriteLine("FAILED: There were problems increasing the price of Margherita pizzas.");

                    Console.WriteLine();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static void UpdateWithParamsExample(string pizzaName)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    connection.Open();

                    string sqlText = "UPDATE pizza SET price = price * 1.1 WHERE name = @pizzaName";

                    SqlCommand cmd = new SqlCommand(sqlText, connection);

                    SqlParameter nameParam = new SqlParameter() {
                        ParameterName = "@pizzaName",
                        Value = pizzaName,
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 25
                    };

                    cmd.Parameters.Add(nameParam);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                        Console.WriteLine($"SUCCESS: Price of {pizzaName} pizza increased.");
                    else
                        Console.WriteLine($"FAILED: There were problems increasing the price of {pizzaName} pizzas.");

                    Console.WriteLine();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static void DeleteExample()
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    connection.Open();

                    string pizzaName = "Worst Pizza";

                    string sqlText = $"DELETE FROM pizza WHERE Name = '{pizzaName}'";

                    SqlCommand cmd = new SqlCommand(sqlText, connection);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                        Console.WriteLine($"SUCCESS: Deleted pizza {pizzaName}.");
                    else
                        Console.WriteLine($"FAILED: There were problems deleting pizza {pizzaName}.");

                    Console.WriteLine();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static void DeleteWithParamsExample(string ingredientName)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    connection.Open();

                    string sqlText = "BEGIN TRANSACTION;" +
                        "declare @deleted table (pizza_id TINYINT);" +
                        "DELETE PXI " +
                        "OUTPUT deleted.pizza_id INTO @deleted " +
                        "FROM pizza AS P " +
                        "JOIN pizza_x_ingredient AS PXI ON P.id = PXI.pizza_id " +
                        "JOIN ingredient AS I ON I.id = PXI.ingredient_id " +
                        "WHERE I.name = @ingredient_name;" +
                        "DELETE P " +
                        "FROM pizza AS P " +
                        "JOIN @deleted AS D ON D.pizza_id = P.id;" +
                        "COMMIT TRANSACTION;";

                    SqlCommand cmd = new SqlCommand(sqlText, connection);

                    SqlParameter nameParam = new SqlParameter() {
                        ParameterName = "@ingredient_name",
                        Value = ingredientName,
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50
                    };

                    cmd.Parameters.Add(nameParam);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1)
                        Console.WriteLine($"SUCCESS: Deleted pizzas with {ingredientName}.");
                    else
                        Console.WriteLine($"FAILED: There were problems deleting pizzas with {ingredientName}.");

                    Console.WriteLine();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public static void MultipleResultSetsExample()
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    connection.Open();

                    string sqlText = "SELECT * FROM pizza; SELECT * FROM ingredient";

                    SqlCommand cmd = new SqlCommand(sqlText, connection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    int idx = 0;

                    while (reader.HasRows)
                    {
                        Console.WriteLine($"--- Result Set {idx + 1}");
                        Console.WriteLine("Too lazy to display records :P");
                        reader.NextResult();
                        idx++;
                    }

                    Console.WriteLine();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                }
            }
        }
    }
}