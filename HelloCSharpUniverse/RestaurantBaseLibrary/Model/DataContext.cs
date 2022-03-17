using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public class DataContext : IDataContext
    {
        private const string FILE_NAME = "data/restaurant_state.bin";
        private const string VERSION = "1.0.0.0";

        public RestaurantDataContext Context;

        public DataContext()
        {
            Context = new RestaurantDataContext();
        }
        public bool Save(out string msg)
        {
            msg = null;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;
            try
            {
                using (stream = File.Open(FILE_NAME, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, VERSION);
                    formatter.Serialize(stream, Context);
                }
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        public bool Load(out string msg)
        {
            msg = null;
            BinaryFormatter formatter = new BinaryFormatter();
            string version = null;

            try
            {
                using (var stream = File.Open(FILE_NAME, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    version = (string)formatter.Deserialize(stream);
                    Context = (RestaurantDataContext)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (version != VERSION)
                throw new ArgumentException(
                    "Impossible to read state from previous format version." +
                    $"Expected: {VERSION}, Actual: {version}");

            return true;
        }

        [Serializable]
        public class RestaurantDataContext
        {
            public IList<MenuItem> MenuItems { get; }
            public IList<MenuItemCategory> MenuCategories { get; }
            public IList<Table> Tables { get; }
            public IList<Invoice> Invoices { get; }
            public IList<Payment> Payments { get; }

            public RestaurantDataContext()
            {
                MenuItems = new List<MenuItem>();
                MenuCategories = new List<MenuItemCategory>();
                Tables = new List<Table>();
                Invoices = new List<Invoice>();
                Payments = new List<Payment>();
            }
        }
    }
}