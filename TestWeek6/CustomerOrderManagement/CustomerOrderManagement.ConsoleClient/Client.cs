using CustomerOrderManagement.ConsoleClient.Model;
using CustomerOrderManagement.Core.Entities;
using CustomerOrderManagement.Services.WCF;
using EasyConsoleFramework.IO;
using EasyConsoleFramework.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderManagement.ConsoleClient
{
    internal class Client : IDisposable
    {
        private HttpClient _orderClient;
        private CustomerService _customerClient;

        public Client(IConfigurationRoot config)
        {
            _orderClient = new HttpClient();
            _orderClient.BaseAddress = new Uri($"{config.GetSection(Constants.BASE_URI_IDENTIFIER).Value}");
            _customerClient = new CustomerService();
        }

        internal void InsertCustomer()
        {
            string code = ReadCustomerCode();
            string firstName = ReadCustomerFirstName();
            string lastName = ReadCustomerLastName();

            Customer customer = new Customer()
            {
                Code = code,
                FirstName = firstName,
                LastName = lastName
            };

            try
            {
                _customerClient.AddCustomer(customer);
                Console.WriteLine("Done!");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                ErrorHandling.Catch(ex);
            }
        }

        internal void ListAllCustomers()
        {
            var customers = _customerClient.FetchCustomers();

            PrintCustomerList(customers as List<Customer>);
        }

        internal void RemoveCustomerById()
        {
            int id = ReadCustomerId();

            try
            {
                _customerClient.RemoveCustomerById(id);
                Console.WriteLine("Done!");
                Console.WriteLine();
            }
            catch(Exception ex)
            {
                ErrorHandling.Catch(ex);
            }
        }

        internal void UpdateCustomer()
        {
            int id = ReadCustomerId();

            try
            {
                Customer customer = _customerClient.GetCustomer(id);

                string code = ReadCustomerCode();
                string firstName = ReadCustomerFirstName();
                string lastName = ReadCustomerLastName();

                customer.Code = code;
                customer.FirstName = firstName;
                customer.LastName = lastName;

                Console.WriteLine("Done!");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                ErrorHandling.Catch(ex);
            }
        }

        internal async Task InsertOrder()
        {
            DateTime date = ReadOrderDate();
            string code = ReadOrderCode();
            string productCode = ReadOrderProductCode();
            decimal total = ReadOrderTotal();
            int customerId = ReadCustomerId();

            OrderContract order = new OrderContract
            {
                Date = date,
                Code = code,
                ProductCode = productCode,
                Total = total,
                CustomerId = customerId
            };

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _orderClient.PostAsync("api/Order", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Done!");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Failed.");
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.Catch(ex);
            }
        }

        internal async Task PrintOrderDetails()
        {
            int id = ReadOrderId();

            try
            {
                HttpResponseMessage response = await _orderClient.GetAsync($"api/Order/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var order = JsonConvert.DeserializeObject<OrderContract>(jsonResponse);

                    StringBuilder sb = new StringBuilder();

                    //sb.AppendLine($"\tOrder ID: {order.Id}"); // Will be displayed here anyway
                    sb.AppendLine($"\tOrder Date: {order.Date.ToString("d")}");
                    sb.AppendLine($"\tOrder Code: {order.Code}");
                    sb.AppendLine($"\tOrder Product Code: {order.ProductCode}");
                    sb.AppendLine($"\tOrder Total: {order.Total.ToString("F2")}");

                    Console.WriteLine(sb.ToString());
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

        internal async Task ListAllOrders()
        {
            try
            {
                HttpResponseMessage response = await _orderClient.GetAsync($"api/Order");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<OrderContract>>(jsonResponse);
                    
                    PrintOrderList(data);
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

        internal async Task ListOrdersForCustomer()
        {
            int customerId = ReadCustomerId();

            try
            {
                HttpResponseMessage response = await _orderClient.GetAsync($"api/Order/bycustomerid/{customerId}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<OrderContract>>(jsonResponse);
                    PrintOrderList(data);
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

        internal async Task UpdateOrder()
        {
            try
            {
                int id = ReadOrderId();

                HttpResponseMessage response = await _orderClient.GetAsync($"api/Order/{id}");

                string jsonResponse = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Failed");
                    Console.WriteLine();
                    return;
                }

                var order = JsonConvert.DeserializeObject<OrderContract>(jsonResponse);

                DateTime date = ReadOrderDate();
                string code = ReadOrderCode();
                string productCode = ReadOrderProductCode();
                decimal total = ReadOrderTotal();
                int customerId = ReadCustomerId();

                order.Date = date;
                order.Code = code;
                order.ProductCode = productCode;
                order.Total = total;
                order.CustomerId = customerId;

                var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await _orderClient.PutAsync("api/Order", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Done!");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Failed.");
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.Catch(ex);
            }
        }

        internal async Task RemoveOrder()
        {
            int id = ReadOrderId();

            HttpResponseMessage response = await _orderClient.DeleteAsync($"api/Order/{id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Done!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Failed.");
            }
        }

        internal async Task ReportOrdersByYear()
        {
            int year = ReadYear();

            try
            {
                HttpResponseMessage response = await _orderClient.GetAsync($"api/Order/byyear/{year}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var orders = JsonConvert.DeserializeObject<List<OrderContract>>(jsonResponse);

                    int nOrders = orders.Count;
                    decimal total = orders.Sum(o => o.Total);

                    Console.WriteLine($"\tNumber of orders for year {year}: {nOrders}");
                    Console.WriteLine($"\tTotal earnings for year {year}: {total.ToString("F2")}");

                    Console.WriteLine();
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

        #region ========================= NOT FOR USERS =========================

        public void Dispose()
        {
            _orderClient.Dispose();
            _orderClient = null;
        }

        private void PrintOrderList(List<OrderContract> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                Console.WriteLine("Found no order.");
                Console.WriteLine();
                return;
            }

            const byte idStringLength = 10;
            const byte dateStringLength = 20;
            const byte codeStringLength = 25;
            const byte productCodeStringLength = 25;
            const byte totalStringLength = 20;
            const byte customerIdStringLength = 10;

            string header = $"{"ID",idStringLength}   {"Date",-dateStringLength}" +
                $"{"Code",-codeStringLength}{"Product code",-productCodeStringLength}" +
                $"{"Total",-totalStringLength}{"Customer ID",-customerIdStringLength}";

            string line = new string('-', Console.BufferWidth);

            Console.WriteLine();
            Console.WriteLine(line);
            Console.WriteLine(header);
            Console.WriteLine(line);

            orders.ForEach(o =>
                Console.WriteLine(
                    $"{o.Id,idStringLength}   {o.Date.ToString("d"),-dateStringLength}" +
                    $"{o.Code,-codeStringLength}{o.ProductCode,-productCodeStringLength}" +
                    $"{o.Total.ToString("F2"),-totalStringLength}{o.CustomerId,-customerIdStringLength}"));

            Console.WriteLine(line);
            Console.WriteLine();
        }

        private void PrintCustomerList(List<Customer> customers)
        {
            if (customers == null  || customers.Count == 0)
            {
                Console.WriteLine("Found no customer.");
                Console.WriteLine();
                return;
            }    

            const byte idStringLength = 10;
            const byte codeStringLength = 40;
            const byte firstNameStringLenth = 30;
            const byte lastNameStringLength = 30;

            string header = $"{"ID",idStringLength}   {"Code",-codeStringLength}" +
                $"{"First Name",-firstNameStringLenth}{"Last Name",-lastNameStringLength}";

            string line = new string('-', Console.BufferWidth);

            Console.WriteLine();
            Console.WriteLine(line);
            Console.WriteLine(header);
            Console.WriteLine(line);

            customers.ForEach(c =>
                Console.WriteLine(
                    $"{c.Id,idStringLength}   {c.Code,-codeStringLength}" +
                    $"{c.FirstName,-firstNameStringLenth}{c.LastName,-lastNameStringLength}"));

            Console.WriteLine(line);
            Console.WriteLine();
        }

        private int ReadOrderId()
        {
            int id = 0;

            BaseIO.ReadFromConsole(
                "\tOrder ID: ",
                s => int.TryParse(s, out id));

            return id;
        }

        private DateTime ReadOrderDate()
        {
            DateTime date = default(DateTime);

            BaseIO.ReadFromConsole(
                "\tOrder Date: ",
                s => DateTime.TryParse(s, out date));

            return date;
        }

        private string ReadOrderCode()
        {
            return BaseIO.ReadFromConsole(
                "\tOrder Code: ",
                s => !String.IsNullOrEmpty(s));
        }

        private string ReadOrderProductCode()
        {
            return BaseIO.ReadFromConsole(
                "\tOrder Product Code: ",
                s => !String.IsNullOrEmpty(s));
        }

        private decimal ReadOrderTotal()
        {
            decimal total = 0;

            BaseIO.ReadFromConsole(
                "\tTotal: ",
                s => decimal.TryParse(s, out total));

            return total;
        }

        private int ReadCustomerId()
        {
            int id = 0;

            BaseIO.ReadFromConsole(
                "\tCustomer ID: ",
                s => int.TryParse(s, out id));

            return id;
        }

        private string ReadCustomerCode()
        {
            return BaseIO.ReadFromConsole(
                "\tCustomer Code: ",
                s => !String.IsNullOrEmpty(s));
        }

        private string ReadCustomerFirstName()
        {
            return BaseIO.ReadFromConsole(
                "\tCustomer First Name: ",
                s => !String.IsNullOrEmpty(s));
        }

        private string ReadCustomerLastName()
        {
            return BaseIO.ReadFromConsole(
                "\tCustomer Last Name: ",
                s => !String.IsNullOrEmpty(s));
        }

        private int ReadYear()
        {
            int year = 0;

            BaseIO.ReadFromConsole(
                "\tYear: ",
                s => int.TryParse(s, out year) && year > 0);

            return year;
        }

        #endregion ========================= NOT FOR USERS =========================
    }
}