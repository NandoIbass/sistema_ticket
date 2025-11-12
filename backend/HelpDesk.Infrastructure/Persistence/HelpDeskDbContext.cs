using HelpDesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Persistence
{
    public class HelpDeskDbContext : DbContext
    {
        public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed inicial (opcional, útil para pruebas)
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    Title = "Ticket inicial",
                    Description = "Ticket de ejemplo creado por seed",
                    Priority = "Media",
                    Status = "Nuevo",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}