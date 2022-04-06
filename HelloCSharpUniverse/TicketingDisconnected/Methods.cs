using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using EasyConsoleFramework.IO;

namespace TicketingDisconnected
{
    public static class Methods
    {
        public static void PrintAllTickets(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();

                try
                {
                    var adapter = InitDataSetAndDataAdapter(ds, connection);

                    DataTable ticket = ds.Tables["ticket"];
                    var ticketRows = ticket.Rows;

                    DataTable category = ds.Tables["category"];
                    var categoryRows = category.Rows;

                    Console.WriteLine();
                    Console.WriteLine(new String('-', Console.BufferWidth));

                    Console.WriteLine("{0,5} {1,-54} {2,-20} {3,-17} {4,-10} {5,-8}",
                        ticket.Columns[0], ticket.Columns[1], ticket.Columns[2], ticket.Columns[3], ticket.Columns[4], ticket.Columns[5]);

                    Console.WriteLine(new String('-', Console.BufferWidth));

                    foreach (DataRow row in ticketRows)
                        Console.WriteLine("{0,5} {1,-54} {2,-20} {3,-17} {4,-10} {5,-8}",
                            row["id"], row["description"], row["opened"], row["customer"], row["state"], row["category_id"]);

                    Console.WriteLine(new String('-', Console.BufferWidth));
                    Console.WriteLine();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL ERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"GENERIC ERROR: {ex.Message}");
                }
            }
        }

        public static void InsertTicket(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    DataSet ds = new DataSet();
                    var adapter = InitDataSetAndDataAdapter(ds, connection);

                    var table = ds.Tables["ticket"];

                    DataRow newRow = table.NewRow();
                    newRow["id"] = 0;
                    newRow["name"] = "Artichokes";
                    newRow["price"] = 7.5m;
                    table.Rows.Add(newRow);

                    DataRow record = table.NewRow();

                    record["description"] = BaseIO.ReadFromConsole("Description: ",
                        s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

                    record["customer"] = BaseIO.ReadFromConsole("Customer: ",
                        s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

                    List<string> availableStates = new List<string>{ "new", "ongoing", "resolved" };

                    record["state"] = BaseIO.ReadFromConsole("State {new, ongoing, resolved}: ",
                        s => availableStates.Contains(s));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }

        public static void DeleteTicket(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
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

        public static void UpdateDatabase(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    DataSet ds = new DataSet();
                    var adapter = InitDataSetAndDataAdapter(ds, connection);

                    adapter.Update(ds, "ticket");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }

        private static SqlDataAdapter InitDataSetAndDataAdapter(
            DataSet ds, SqlConnection connection, SqlTransaction transaction = null)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            // ========================= SELECT COMMAND =========================
            adapter.SelectCommand = new SqlCommand(
                "SELECT * FROM ticket AS T LEFT JOIN category AS C ON T.category_id = C.id ORDER BY opened DESC",
                connection);

            // ========================= INSERT COMMAND =========================

            SqlCommand insertCmd = connection.CreateCommand();

            insertCmd.CommandText = "INSERT INTO ticket VALUES(@description, GETDATE(), @customer, @state)";
            insertCmd.CommandType = CommandType.Text;
            insertCmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@description",
                SqlDbType = SqlDbType.VarChar,
                Size = 500,
                SourceColumn = "description"
            });
            insertCmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@customer",
                SqlDbType = SqlDbType.VarChar,
                Size = 100,
                SourceColumn = "customer"
            });
            insertCmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@state",
                SqlDbType = SqlDbType.VarChar,
                Size = 10,
                SourceColumn = "state"
            });

            adapter.InsertCommand = insertCmd;

            // ========================= DELETE COMMAND =========================

            SqlCommand deleteCmd = connection.CreateCommand();

            deleteCmd.CommandText = "DELETE FROM pizza WHERE id = @id";
            deleteCmd.CommandType = CommandType.Text;
            deleteCmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.TinyInt,
                SourceColumn = "id"
            });

            adapter.DeleteCommand = deleteCmd;

            // ========================= ADDITIONAL OPTIONS =========================

            // Add all constraints, including keys
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            // ========================= FILL DATASETS =========================

            adapter.Fill(ds, "category");
            adapter.Fill(ds, "ticket");

            return adapter;
        }
    }
}