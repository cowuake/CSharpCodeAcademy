using Library.DesktopClient.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.DesktopClient
{
    public class WebApiClient : IDisposable
    {
        private HttpClient _client;

        public WebApiClient()
        {
            _client = new HttpClient();
            //_client.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_base_address"].ToString());
            _client.BaseAddress = new Uri("https://localhost:44375/");
        }

        public async Task<IEnumerable<BookModel>> GetBooksAsync(BookFilterModel filter = null)
        {
            HttpResponseMessage response = null;

            if (filter == null)
            {
                response = _client.GetAsync("api/Library").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    return JsonConvert.DeserializeObject<IEnumerable<BookModel>>(responseContent);
                }
            }
            else if (!string.IsNullOrEmpty(filter.Isbn))
            {
                response = _client.GetAsync($"api/ByISBN/{filter.Isbn}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    return JsonConvert.DeserializeObject<IEnumerable<BookModel>>(responseContent);
                }
            }
            else if (!string.IsNullOrEmpty(filter.Title))
            {
                response = _client.GetAsync($"api/ByTitle/{filter.Isbn}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    return JsonConvert.DeserializeObject<IEnumerable<BookModel>>(responseContent);
                }
            }
        }

        public async Task<bool> InsertBook(BookModel book)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(book),
                Encoding.UTF8,
                "application/json"
                );

            HttpResponseMessage response = await _client.PostAsync("api/Library", content);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        //public async Task<IEnumerable<BookGenreContract>> GetBookGenres()
        //{
        //    HttpResponseMessage response = await _client.GetAsync("api/BookGenre");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseContent = response.Content.ReadAsStringAsync().Result;

        //        JsonConvert.DeserializeObject<IEnumerable<BookGenreContract>>(responseContent);
        //    }

        //    return new List<BookGenreContract>();
        //}

        public void Dispose()
        {
            _client?.Dispose();
            _client = null;
        }
    }
}
