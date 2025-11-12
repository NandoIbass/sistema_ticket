using System;

namespace HelpDesk.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = "Baja"; // Baja/Media/Alta
        public string Status { get; set; } = "Nuevo"; // Nuevo/En Progreso/Resuelto
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? AssignedUser { get; set; }
    }
}