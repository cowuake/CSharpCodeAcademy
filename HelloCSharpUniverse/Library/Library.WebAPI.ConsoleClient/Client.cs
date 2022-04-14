using EasyConsoleFramework.IO;
using EasyConsoleFramework.Utils;
using Library.WebAPI.ConsoleClient.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.WebAPI.ConsoleClient
{
    internal class Client : IDisposable
    {
        private HttpClient _http;

        public Client(IConfigurationRoot config)
        {
            _http = new HttpClient();
            _http.BaseAddress = new Uri($"{config.GetSection(Constants.BASE_URI_IDENTIFIER).Value}");
        }

        #region ========================= INTERNAL METHODS =========================

        internal async Task ListAllBooks()
        {
            //HttpClient httpClient = new HttpClient();

            //HttpRequestMessage request = new HttpRequestMessage()
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri("https://localhost:44375/api/library")
            //};

            HttpResponseMessage response = null;

            try
            {
                response = await _http.GetAsync("api/library");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<BookContract>>(jsonResponse);
                    PrintBookList(data);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.Catch(ex);
            } 
        }

        internal async Task InsertBook()
        {
            string isbn, title, author, summary;

            isbn = BaseIO.ReadFromConsole(
                "Book ISBN: ",
                s => !String.IsNullOrEmpty(s));

            title = BaseIO.ReadFromConsole(
                "Book title: ",
                s => !String.IsNullOrEmpty(s));

            author = BaseIO.ReadFromConsole(
                "Book author: ",
                s => !String.IsNullOrEmpty(s));

            summary = BaseIO.ReadFromConsole(
                "Book summary: ",
                s => !String.IsNullOrEmpty(s));
        }

        #endregion ========================= INTERNAL METHODS =========================

        #region ========================= PRIVATE METHODS =========================

        private void PrintBookList(List<BookContract> books)
        {
            const byte isbnStringLength = 15;
            const byte titleStringLength = 35;
            const byte authorStringLength = 20;
            const byte summaryStringLength = 47;

            string header = $"{"ISBN", isbnStringLength}   {"Title", -titleStringLength}" +
                $"{"Author", -authorStringLength}{"Summary", -summaryStringLength}";

            string line = new string('-', Console.BufferWidth);

            Console.WriteLine();
            Console.WriteLine(line);
            Console.WriteLine(header);
            Console.WriteLine(line);

            books.ForEach(b =>
                Console.WriteLine(
                    $"{b.ISBN, isbnStringLength}   {b.Title,-titleStringLength}" +
                    $"{b.Author,-authorStringLength}{b.Summary,-summaryStringLength}"));

            Console.WriteLine(line);
            Console.WriteLine();
        }

        #endregion ========================= PRIVATE METHODS =========================

        public void Dispose()
        {
            _http.Dispose();
            _http = null;
        }
    }
}