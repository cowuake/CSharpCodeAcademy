namespace HelpDesk.Client.Facilities
{
    public static class Utils
    {
        public static string ReadFromConsole(string msg, Func<string, bool> condition = null)
        {
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine().Trim();
            }
            while (!condition(input));

            return input;
        }
    }
}