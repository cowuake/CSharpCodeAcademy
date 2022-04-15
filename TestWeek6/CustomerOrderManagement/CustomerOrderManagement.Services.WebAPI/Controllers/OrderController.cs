using CustomerOrderManagement.Core.Entities;
using CustomerOrderManagement.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrderManagement.Services.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMainBusinessLogic _logic;

        public OrderController(IMainBusinessLogic logic) // Constructor injection
        {
            _logic = logic;
        }

        /// <summary>
        /// Fetches all orders
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Order>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult FetchOrders()
        {
            var result = _logic.FetchOrders();

            if (result == null)
                return NotFound("Order not found."); // Error 404

            return Ok(result);
        }

        /// <summary>
        /// Gets order by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult GetOrder(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid order ID."); // Error 400

            var result = _logic.GetOrder(id);

            if (result == null)
                return NotFound("No order found."); // Error 404

            return Ok(result);
        }

        /// <summary>
        /// Fetch order performed in a specific year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("byyear/{year}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Order>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult FetchOrdersByYear(int year)
        {
            if (year < 0)
                return BadRequest("Invalid year."); // Error 400

            var result = _logic.FetchOrders(o => o.Date.Year == year);

            if (result == null)
                return NotFound("No order found."); // Error 404

            return Ok(result);
        }

        /// <summary>
        /// Fetches order by a specific customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("bycustomerid/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Order>))]
        public IActionResult FetchOrdersByCustomer(int customerId)
        {
            var result = _logic.FetchOrders(o => o.CustomerId == customerId).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Removes an order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult RemoveOrder(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid order ID."); // Error 400

            var result = _logic.RemoveOrderById(id);

            if (!result)
                return StatusCode(500, "Cannot remove order.");

            return Ok(result);
        }

        /// <summary>
        /// Add an order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddOrder([FromBody] Order order)
        {
            if (order == null)
                return BadRequest("Invalid order data.");

            bool result = _logic.AddOrder(order);

            if (!result)
                return StatusCode(500, "Cannot insert order.");

            return NoContent(); // 204
        }

        /// <summary>
        /// Updates an order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult UpdateOrder(int id, Order order)
        {
            if (id <= 0 || order == null)
                return BadRequest("Invalid parameters.");

            if (id != order.Id)
                return BadRequest("Order ID does not match.");

            var result = _logic.UpdateOrder(order);

            if (!result)
                return StatusCode(500, "Cannot update order data.");

            return Ok(order);
        }
    }
}