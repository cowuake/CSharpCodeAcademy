using System.Text;

namespace ExtensionLibrary
{
    public static class StringExtensions
    {
        public static string RepeatString(this string s, int n)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                sb.Append(s);
            }

            return sb.ToString();
        }
    }
}
