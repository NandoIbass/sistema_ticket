using System;

namespace HelpDesk.Application.DTOs
{
    public record TicketDto(int Id, string Title, string Description, string Priority, string Status, DateTime CreatedAt, string? AssignedUser);
    public record CreateTicketDto(string Title, string Description, string Priority, string? AssignedUser);
    public record UpdateTicketDto(string Title, string Description, string Priority, string Status, string? AssignedUser);
}