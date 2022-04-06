using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using EasyConsoleFramework.IO;

namespace TicketingDisconnected
{
    public class MethodProvider
    {
        private readonly SqlDataAdapter _adapter;
        private readonly SqlConnection _connection;
        private readonly DataSet _dataSet;

        internal MethodProvider(string connectionString)
        {
            _dataSet = new DataSet();
            _connection = new SqlConnection(connectionString);
            _adapter = InitDataSetAndDataAdapter();
        }

        public void PrintAllTickets()
        {
            try
            {
                DataTable ticket = _dataSet.Tables["ticket"];
                var ticketRows = ticket.Rows;

                Console.WriteLine();
                Console.WriteLine(new String('-', Console.BufferWidth));

                Console.WriteLine("{0,5} {1,-55} {2,-12} {3,-17} {4,-10} {5,-8}",
                    ticket.Columns[0], ticket.Columns[1], ticket.Columns[2],
                    ticket.Columns[3], ticket.Columns[4], ticket.Columns[5]);

                Console.WriteLine(new String('-', Console.BufferWidth));

                foreach (DataRow row in ticketRows)
                    Console.WriteLine("{0,5} {1,-55} {2,-12} {3,-17} {4,-10} {5,-8}",
                        row["id"], row["description"], Convert.ToDateTime(row["opened"]).ToString("yyyy/MM/dd"),
                        row["customer"], row["state"], row["category"]);

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

        public void InsertTicket()
        {
            try
            {
                var table = _dataSet.Tables["ticket"];

                DataRow record = table.NewRow();

                record["description"] = BaseIO.ReadFromConsole(
                    "Description: ",
                    s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

                record["customer"] = BaseIO.ReadFromConsole(
                    "Customer: ",
                    s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

                List<string> availableStates = new List<string> { "new", "ongoing", "resolved" };

                record["state"] = "new";

                _dataSet.Tables["ticket"].Rows.Add(record);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void DeleteTicket()
        {
            try
            {
                var table = _dataSet.Tables["pizza"];
                DataRow toDel = table.Rows.Find(5); // Find searches by primary key
                toDel?.Delete(); // ?. => NULL PROPAGATION

                foreach (DataRow row in table.Rows)
                    if (row.RowState != DataRowState.Deleted)
                        Console.WriteLine($"{row["name"]} {row["name"]} {row["price"]}");
                    else
                        Console.WriteLine("Deleting...");

                _adapter.Update(_dataSet, "pizza");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void UpdateDatabase()
        {
        }

        private SqlDataAdapter InitDataSetAndDataAdapter()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            // ========================= SELECT COMMAND =========================

            string selectText =
                @"SELECT T.id AS id, T.description, T.opened, T.customer, T.state, C.id AS category_id, C.name AS category
                FROM ticket AS T
                LEFT JOIN category AS C ON T.category_id = C.id
                ORDER BY opened DESC";

            adapter.SelectCommand = new SqlCommand(selectText, _connection);

            // ========================= INSERT COMMAND =========================

            SqlCommand insertCmd = _connection.CreateCommand();

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

            SqlCommand deleteCmd = _connection.CreateCommand();

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

            adapter.Fill(_dataSet, "ticket");

            // ========================= RETURN =========================

            return adapter;
        }

        private void Refresh()
        {
            // Update local dataset
            _adapter.Update(_dataSet, "ticket");

            // Reset dataset
            _dataSet.Reset();

            // Fill dataset with fresh data
            _adapter.Fill(_dataSet, "ticket");
        }
    }
}