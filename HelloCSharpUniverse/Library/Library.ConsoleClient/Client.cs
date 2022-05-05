using Library.ConsoleClient;
using EasyConsoleFramework.Extensions;
using EasyConsoleFramework.Utils;
using Library.ConsoleClient.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.ConsoleClient
{
    internal class Client : IDisposable
    {
        private HttpClient _client;

        #region ========================= CONSTRUCTORS =========================

        public Client(IConfigurationRoot config)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"{config.GetSection(Constants.BASE_URI_IDENTIFIER).Value}");
        }

        #endregion ========================= CONSTRUCTORS =========================

        #region ========================= CLIENT METHODS =========================

        internal async Task ListAllBooks()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/Library");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<BookContract>>(jsonResponse);

                    PrintBookList(data);
                }
                else
                {
                    Console.WriteLine("Failed.");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.Catch(ex);
            }
        }

        #endregion ========================= CLIENT METHODS =========================

        #region ========================= PRINTING UTILITIES =========================

        private void PrintBookList(List<BookContract> books)
        {
            if (books == null || books.Count == 0)
            {
                Console.WriteLine("Found no order.");
                Console.WriteLine();
                return;
            }

            const byte isbnStringLength = 10;
            const byte authorStringLength = 20;
            const byte titleStringLength = 25;
            const byte pagesStringLength = 10;
            const byte bookGenreIdStringLength = 38;
            const byte categoryIdStringLength = 7;

            string header = $"{"ID",isbnStringLength}   {"Date",-authorStringLength}" +
                $"{"Code",-titleStringLength}{"Product code",-pagesStringLength}" +
                $"{"Total",-bookGenreIdStringLength}{"Customer ID",-categoryIdStringLength}";

            string line = new string('-', Console.BufferWidth);

            Console.WriteLine();
            Console.WriteLine(line);
            Console.WriteLine(header);
            Console.WriteLine(line);

            books.ForEach(o =>
                Console.WriteLine(
                    $"{o.ISBN,isbnStringLength}   {o.Author,-authorStringLength}" +
                    $"{o.Title,-titleStringLength}{o.Pages,-pagesStringLength}" +
                    $"{o.Summary,-bookGenreIdStringLength}{o.BookGenreId,-bookGenreIdStringLength}"));

            Console.WriteLine(line);
            Console.WriteLine();

            //books.ToFormattedString(new List<int> { 10, 20, 25, 10, 38, 7 });
        }

        #endregion ========================= PRINTING UTILITIES =========================

        #region ========================= MEMORY MANAGEMENT =========================

        public void Dispose()
        {
            _client.Dispose();
            _client = null;
        }

        #endregion ========================= MEMORY MANAGEMENT =========================
    }
}
