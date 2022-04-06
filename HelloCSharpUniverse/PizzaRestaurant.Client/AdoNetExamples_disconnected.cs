using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using MainLibrary;
using System.Data;

namespace PizzaRestaurant.Client
{
    public static class AdoNetExamples_disconnected
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

        public static void ReadDataExample()
        {
            using (var connection = new SqlConnection(cs))
            {
                DataSet ds = new DataSet();
                //var adapter = new SqlDataAdapter();

                try
                {
                    //adapter.SelectCommand = new SqlCommand(
                    //    "SELECT * FROM pizza",
                    //    connection);
                    var adapter = InitDataSetAndDataAdapter(ds, connection);

                    // This also opens the connection
                    // ... and reads the database
                    // ... and capture its content in a DataSet
                    // ... and close the connection
                    //adapter.Fill(ds, "pizza");

                    //adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    DataTable table = ds.Tables["pizza"];
                    var rows = table.Rows;

                    foreach (DataColumn column in table.Columns)
                        Console.WriteLine($"{column.ColumnName} - {column.DataType}");

                    Console.WriteLine();

                    foreach (Constraint constraint in table.Constraints)
                        Console.WriteLine($"{constraint.ConstraintName} - {constraint.ExtendedProperties}");

                    Console.WriteLine();

                    foreach (DataRow row in rows)
                        Console.WriteLine("{0,5} {1,-25} {2,10}", row[0], row[1], row["price"]);


                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"GENERIC ERROR: {ex.Message}");
                }
                // finally not needed, the connection will be automatically closed
            }
        }

        public static void InsertExample()
        {
            using (var connection = new SqlConnection(cs))
            {
                try
                {
                    DataSet ds = new DataSet();
                    var adapter = InitDataSetAndDataAdapter(ds, connection);

                    var table = ds.Tables["pizza"];

                    DataRow newRow = table.NewRow();
                    newRow["id"] = 0;
                    newRow["name"] = "Artichokes";
                    newRow["price"] = 7.5m;
                    table.Rows.Add(newRow);

                    DataRow newRow2 = table.NewRow();
                    newRow["id"] = 0;
                    newRow["name"] = "Bresaola";
                    newRow["price"] = 12m;
                    table.Rows.Add(newRow);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }

        public static void UpdateExample()
        {
            using (var connection = new SqlConnection(cs))
            {
                try
                {
                    DataSet ds = new DataSet();
                    var adapter = InitDataSetAndDataAdapter(ds, connection);

                    var table = ds.Tables["pizza"];
                    var firstRow = table.Rows[0];
                    firstRow["Name"] = "Dummy Pizza";

                    adapter.Update(ds, "pizza");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }

        public static void DeleteExample()
        {
            using (var connection = new SqlConnection(cs))
            {
                try
                {
                    DataSet ds = new DataSet();
                    var adapter = InitDataSetAndDataAdapter(ds, connection);

                    var table = ds.Tables["pizza"];
                    DataRow toDel = table.Rows.Find(5); // Find searches by primary key
                    toDel?.Delete(); // ?. => NULL PROPAGATION

                    foreach (DataRow row in table.Rows)
                        if (row.RowState != DataRowState.Deleted)
                            Console.WriteLine($"{row["name"]} {row["name"]} {row["price"]}");
                        else
                            Console.WriteLine("Deleting...");

                    adapter.Update(ds, "pizza");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }

        public static void TransactionExample()
        {
            using (var connection = new SqlConnection(cs))
            {
                SqlTransaction transaction = null;

                try
                {
                    connection.Open();
                    // BEGIN TRANSACTION
                    transaction = connection.BeginTransaction();

                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = InitDataSetAndDataAdapter(ds, connection, transaction);

                    // Add new record
                    DataRow newRow = ds.Tables["pizza"].NewRow();
                    newRow["id"] = 0;
                    newRow["name"] = "Marinara";
                    newRow["price"] = 5m;
                    ds.Tables["pizza"].Rows.Add(newRow);

                    // Modify existing record
                    DataRow toUpd = ds.Tables["pizza"].Rows.Find(1);
                    toUpd["price"] = 8m;

                    // Delete record
                    ds.Tables["pizza"].Rows.Find(30)?.Delete();

                    // Synchronize
                    adapter.Update(ds, "pizza");

                    // COMMIT TRANSACTION
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                    
                    // ROLLBACK TRANSACTION
                    if (transaction != null)
                        transaction.Rollback();
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }

        private static SqlDataAdapter InitDataSetAndDataAdapter(
            DataSet ds, SqlConnection connection, SqlTransaction transaction = null)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = new SqlCommand(
                "SELECT * FROM pizza",
                connection);

            SqlCommand updateCmd = connection.CreateCommand();

            updateCmd.CommandText = "UPDATE pizza SET name = @name WHERE id = @id";
            updateCmd.CommandType = CommandType.Text;
            updateCmd.Parameters.Add(new SqlParameter {
                ParameterName = "@name",
                SqlDbType = SqlDbType.VarChar,
                Size = 100,
                SourceColumn = "name"
            });
            updateCmd.Parameters.Add(new SqlParameter {
                ParameterName = "@id",
                SqlDbType = SqlDbType.TinyInt,
                SourceColumn = "id"
            });

            adapter.UpdateCommand = updateCmd;

            SqlCommand insertCmd = connection.CreateCommand();

            insertCmd.CommandText = "INSERT INTO pizza VALUES(@name, @price)";
            insertCmd.CommandType = CommandType.Text;
            insertCmd.Parameters.Add(new SqlParameter {
                ParameterName = "@name",
                SqlDbType = SqlDbType.VarChar,
                Size = 25,
                SourceColumn = "name"
            });
            insertCmd.Parameters.Add(new SqlParameter {
                ParameterName = "@price",
                SqlDbType = SqlDbType.Money,
                SourceColumn = "price"
            });

            adapter.InsertCommand = insertCmd;

            SqlCommand deleteCmd = connection.CreateCommand();

            deleteCmd.CommandText = "DELETE FROM pizza WHERE id = @id";
            deleteCmd.CommandType = CommandType.Text;
            deleteCmd.Parameters.Add(new SqlParameter {
                ParameterName = "@id",
                SqlDbType = SqlDbType.TinyInt,
                SourceColumn = "id"
            });

            adapter.DeleteCommand = deleteCmd;

            if (transaction != null)
            {
                
            }

            // Add all constraints, including keys
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            // Fill DataSet
            adapter.Fill(ds, "pizza");

            return adapter;
        }
    }
}