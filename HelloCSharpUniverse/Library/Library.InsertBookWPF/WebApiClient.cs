using Library.InsertBookWPF.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.InsertBookWPF
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

        public async Task<bool> InsertBook(BookContract book)
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

        public async Task<IEnumerable<BookGenreContract>> GetBookGenres()
        {
            HttpResponseMessage response = await _client.GetAsync("api/BookGenre");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;

                JsonConvert.DeserializeObject<IEnumerable<BookGenreContract>>(responseContent);
            }

            return new List<BookGenreContract>();
        }

        public void Dispose()
        {
            _client?.Dispose();
            _client = null;
        }
    }
}
