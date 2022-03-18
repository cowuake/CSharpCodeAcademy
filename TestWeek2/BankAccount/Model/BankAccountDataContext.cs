using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BankAccount.Model
{
    public class BankAccountDataContext : IBankAccountDataContext
    {
        public const string DATA_FILE_PATH = "data/rist.bin";
        private const string DATA_FORMAT_VERSION = "0.1.0.0";

        public BankAccountState Data;

        public BankAccountDataContext()
        {
            Data = new BankAccountState();
        }

        public bool Save(out string message)
        {
            message = null;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;
            try
            {
                using (stream = File.Open(DATA_FILE_PATH, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, DATA_FORMAT_VERSION);
                    formatter.Serialize(stream, Data);
                }
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        public bool Load(out string message)
        {
            message = null;

            BinaryFormatter formatter = new BinaryFormatter();

            string version = null;

            try
            {
                using (var stream = File.Open(DATA_FILE_PATH, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    version = (string)formatter.Deserialize(stream);
                    Data = (BankAccountState)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            if (version != DATA_FORMAT_VERSION)
                throw new ArgumentException($"");

            return true;
        }

        [Serializable]
        public class BankAccountState
        {
            public IList<PersonalAccount> Accounts { get; }

            public BankAccountState()
            {
                Accounts = new List<PersonalAccount>();
            }
        }
    }
}
