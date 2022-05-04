using EasyConsoleFramework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConsoleFramework.IO
{
    public static class Helpers
    {
        public static bool ReadYesOrNo(string question = "Do you want to proceed?")
        {
            //string displayChoices = null;

            bool? booleanChoice = null;

            string output = BaseIO.ReadFromConsole(
                $"{question} [Y/N]\t",
                s => Converters.CanBeBool(s, out booleanChoice));

            return booleanChoice.Value;
        }
    }
}