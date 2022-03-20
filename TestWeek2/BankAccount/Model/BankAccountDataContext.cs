using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace BankAccount.Model
{
    public class BankAccountDataContext : IBankAccountDataContext
    {
        private const string DATA_FORMAT_VERSION = "0.1.0.0";

        public BankAccountState Data;
        public BankAccountDataContext() => Data = new BankAccountState();

        #region ====================   Save & Load   ====================
        public bool Save(string path, out string message)
        {
            message = null;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                using (stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
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

        public bool Load(string path, out string message)
        {
            message = null;

            BinaryFormatter formatter = new BinaryFormatter();

            string version = null;

            try
            {
                using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
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
                throw new FileLoadException($"Impossible to read from data file version {version}" +
                    $"Version {DATA_FORMAT_VERSION} expected.");

            return true;
        }
        #endregion

        [Serializable]
        public class BankAccountState
        {
            public List<PersonalAccount> Accounts { get; }

            public BankAccountState()
            {
                Accounts = new List<PersonalAccount>();
            }
        }
    }
}