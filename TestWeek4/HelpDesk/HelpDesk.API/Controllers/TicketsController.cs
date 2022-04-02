using HelpDesk.Core;
using HelpDesk.Core.BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly MainBusinessLayer _businessLayer;

        public TicketsController()
        {
            _businessLayer = new MainBusinessLayer();
        }

        [HttpGet]
        public IActionResult FetchAll()
        {
            var data = this._businessLayer.FetchAll();
            return Ok(data);
        }

        [HttpGet("byid/{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0)
                return BadRequest(string.Empty);

            var data = this._businessLayer.GetById(id);
            return Ok(data);
        }

        [HttpGet("bystate/{state}")]
        public IActionResult GetByState(TicketState state)
        {
            var data = this._businessLayer.GetTicketsByState(state);
            return Ok(data);
        }
    }
}