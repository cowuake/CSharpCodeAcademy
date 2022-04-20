using EasyConsoleFramework.IO;
using Employees.WebAPI.ConsoleClient.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Employees.WebAPI.ConsoleClient.Methods
{
    internal class Client
    {
        internal async Task EmployeeList()
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https//locahost:44375/api/employees")
            };

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<EmployeeContract>>(jsonResponse);
            }
        }

        internal async Task GetEmployeeById()
        {
            int id = 0;

            BaseIO.ReadFromConsole(
                "Insert employee ID: ",
                s => int.TryParse(s, out id));

            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:44375/api/employee/{id}")
            };
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<EmployeeContract>(jsonResponse);

            }
            else
            {

            } 
        }

        internal async Task AddEmployee()
        {
            string code = BaseIO.ReadFromConsole(
                 "\tEmployee's codel: ",
                 s => !String.IsNullOrEmpty(s));

            string firstName = BaseIO.ReadFromConsole(
                 "\tEmployee's first name: ",
                 s => !String.IsNullOrEmpty(s));

            string lastName = BaseIO.ReadFromConsole(
                 "\tEmployee's last name: ",
                 s => !String.IsNullOrEmpty(s));

            decimal annualSalary = 0;

            BaseIO.ReadFromConsole(
                 "\tEmployee's annual salary: ",
                 s => decimal.TryParse(s, out annualSalary));

            EmployeeContract employee = new EmployeeContract
            {
                Code = code,
                FirstName = firstName,
                LastName = lastName,
                AnnualSalary = annualSalary
            };

            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://localhost:44375/api/employee")
            };

            request.Content = new StringContent(
                JsonConvert.SerializeObject(employee),
                Encoding.UTF8,
                "application/json"
            );

            var client = new HttpClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                
            }
            else
            {

            }
        }
    }
}