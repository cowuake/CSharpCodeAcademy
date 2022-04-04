using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Threading.Tasks;

namespace PizzaRestaurant.Client
{
    public static class AdoNetExamples
    {
        public static void ConnectionExample()
        {
            // Connection string
            string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=pizza_restaurant;Integrated Security=True";

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
            string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=pizza_restaurant;Integrated Security=True";

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
            string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=pizza_restaurant;Integrated Security=True";
            using (var connection = new SqlConnection(cs))
            {
                try
                {
                    // Define SQL query to be executed
                    string sqlCommand = "SELECT * FROM pizza";

                    // Open connection
                    connection.Open();

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
    }
}