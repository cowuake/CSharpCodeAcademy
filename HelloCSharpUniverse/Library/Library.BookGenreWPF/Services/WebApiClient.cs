using Library.BookGenreWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.BookGenreWPF.Services
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

        public async Task<IEnumerable<BookGenreModel>> GetBookGenresAsync()
        {
            HttpResponseMessage response = _client.GetAsync("api/BookGenre").Result;

            if (!response.IsSuccessStatusCode)
                return new List<BookGenreModel>();
            
            var responseContent = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<BookGenreModel>>(responseContent);
        }

        public async Task<bool> InsertBookGenre(BookGenreModel genre)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(genre),
                Encoding.UTF8,
                "application/json"
                );

            HttpResponseMessage response = _client.PostAsync("api/BookGenre", content).Result;

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public void Dispose()
        {
            _client?.Dispose();
            _client = null;
        }
    }
}