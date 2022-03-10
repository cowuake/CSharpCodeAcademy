using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AccessSchoolRegister.Model;

namespace AccessSchoolRegister.Services
{
    internal class RegisterToASCII : IRegisterStateHandler
    {
        private const string fileName = "register.txt";

        public bool LoadState()
        {
            throw new NotImplementedException();
        }

        public bool SaveState(SchoolRegister register, out string errorMsg)
        {
            errorMsg = null;

            List<string> stringList = new List<string>();

            try
            {
                File.WriteAllLines(fileName, stringList);
            } catch (Exception e)
            {
                errorMsg = e.Message;
                return false;
            }

            return true;
        }
    }
}
