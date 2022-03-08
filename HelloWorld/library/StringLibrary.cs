using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Library
{
    public class StringLibrary
    {
        public static void ToCamelCase(string input, out string noSpace, out string withSpace)
        {
            // Split input string
            List<string> splitted = input.Split(' ').ToList();

            // Remove additional whitespace
            splitted.RemoveAll(s => String.IsNullOrWhiteSpace(s));

            // Initialize two StringBuilder object for storing intermediate results
            var sb = new StringBuilder();

            // Change first char to upper case for each string
            foreach (string s in splitted)
            {
                sb.Append(s.Substring(0,1).ToUpper() + s.Substring(1) + " ");
            }

            // Assign to out variables
            withSpace = sb.ToString();
            noSpace = withSpace.Replace(" ", "");
        }
    }
}
