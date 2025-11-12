using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Interfaces
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllAsync(int pageSize, string? status = null, string? priority = null, int page = 1);
        Task<Ticket?> GetByIdAsync(int id);
        Task<Ticket> CreateAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(int id);
        Task<int> CountAsync(string? status = null, string? priority = null);
    }
}