using HelpDesk.Application.DTOs;
using HelpDesk.Application.Interfaces;
using HelpDesk.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace HelpDesk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _repo;
        public TicketsController(ITicketRepository repo) { _repo = repo; }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] string? priority, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            var items = await _repo.GetAllAsync(pageSize, status, priority, page);
            var total = await _repo.CountAsync(status, priority);
            return Ok(new { items, total, page, pageSize });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var t = await _repo.GetByIdAsync(id);
            return t == null ? NotFound() : Ok(t);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto dto)
        {
            var entity = new Ticket { Title = dto.Title, Description = dto.Description, Priority = dto.Priority, AssignedUser = dto.AssignedUser, Status = "Abierto"};
            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();
            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Priority = dto.Priority;
            existing.Status = dto.Status;
            existing.AssignedUser = dto.AssignedUser;
            await _repo.UpdateAsync(existing);
            return NoContent();
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
        

    }

}