using HelpDesk.Application.Interfaces;
using HelpDesk.Domain.Entities;
using HelpDesk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HelpDesk.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly HelpDeskDbContext _ctx;
        public TicketRepository(HelpDeskDbContext ctx) { _ctx = ctx; }
        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            _ctx.Tickets.Add(ticket);
            await _ctx.SaveChangesAsync();
            return ticket;

        }


        public async Task DeleteAsync(int id)
        {
            var t = await _ctx.Tickets.FindAsync(id);
            if (t == null) return;
            _ctx.Tickets.Remove(t);
            await _ctx.SaveChangesAsync();


        }

        public async Task<IEnumerable<Ticket>> GetAllAsync(int pageSize, string? status = null, string? priority = null, int page = 1)
        {
            var q = _ctx.Tickets.AsQueryable();
            if (!string.IsNullOrEmpty(status)) q = q.Where(x => x.Status == status);
            if (!string.IsNullOrEmpty(priority)) q = q.Where(x => x.Priority == priority);
            return await q.OrderByDescending(x => x.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id) => await _ctx.Tickets.FindAsync(id);

        public async Task UpdateAsync(Ticket ticket)
        {
            _ctx.Tickets.Update(ticket);
            await _ctx.SaveChangesAsync();

        }
        
        public async Task<int> CountAsync(string? status = null, string? priority = null)
        {
             var q = _ctx.Tickets.AsQueryable();
            if (!string.IsNullOrEmpty(status)) q = q.Where(x => x.Status == status);
            if (!string.IsNullOrEmpty(priority)) q = q.Where(x => x.Priority == priority);
            return await q.CountAsync();
        }


    }

}